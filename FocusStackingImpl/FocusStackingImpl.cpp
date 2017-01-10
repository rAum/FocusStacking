#include <algorithm>
#include <array>
#include <numeric>
#include "FocusStackingImpl.h"
#include "Mat.h"
#include "ThreadPool.h"

using ColorImages = std::vector<ImageRgb24>;
using EdgeImages  = std::vector<ImageGray8>;

FocusStackingImpl::FocusStackingImpl(size_t layersCountHint)
	: reportProgress_(noReporting), useAutomaticThreshold_(true), mergeStrategy_(MergeStrategy::AlwaysMaximum), edgeMethod_(EdgeDetectionMethod::Laplace8)
	, depthThresholdValue_(128)

{
	layers_.reserve(layersCountHint);
}

FocusStackingImpl::~FocusStackingImpl()
{
}

void FocusStackingImpl::clearInputImages()
{
	layers_.clear();
}

	ImageGray8 toGrayscale(ImageRgb24& input)
	{
		ImageGray8 grayscale(input.width(), input.height());

		for (size_t y = 0; y < grayscale.height(); ++y)
		{
			for (size_t x = 0; x < grayscale.width(); ++x)
			{
				auto p = input(y, x);
				float luminance = 0.299f * p.R() + 0.587f * p.G() + 0.114f * p.B();
				grayscale(y, x) = static_cast<uint8_t>(std::min(255.f, luminance));
			}
		}

		return grayscale;
	}

	ImageGray8 sobel(ImageGray8& input)
	{
		ImageGray8 res(input.width(), input.height());

		const size_t w = res.width() - 1;
		const size_t h = res.height() - 1;

		// vertical
		for (size_t y = 1; y < h; ++y)
		{
			res(y, 0) = '\0';
			for (size_t x = 1; x < w; ++x)
			{
				int left = input(y - 1, x - 1);
				int right = input(y - 1, x + 1);

				left  += 2 * input(y, x - 1);
				right += 2 * input(y, x + 1);

				left  += input(y + 1, x - 1);
				right += input(y + 1, x + 1);
				
				res(y, x) = abs(right - left);
			}
			res(y, w) = '\0';
		}

		for (size_t x = 0; x <= w; ++x)
		{
			res(0, x) = '\0';
		}
		for (size_t x = 0; x <= w; ++x)
		{
			res(h, x) = '\0';
		}

		// horizontal
		for (size_t y = 1; y < h; ++y)
		{
			for (size_t x = 1; x < w; ++x)
			{
				int top = input(y - 1, x - 1);
				top += 2 * input(y - 1, x);
				top += input(y - 1, x + 1);

				int bottom = input(y + 1, x - 1);
				bottom += 2 * input(y + 1, x);
				bottom += input(y + 1, x + 1);

				auto p = abs(top - bottom);
				res(y, x) = (res(y, x) + abs(top - bottom)) / 2;
			}
		}

		return res;
	}

	ImageGray8 laplace8(ImageGray8& input)
	{
		ImageGray8 edges(input.width(), input.height());

		const auto h = edges.height() - 1;
		const auto w = edges.width() - 1;

		for (size_t y = 1; y < h; ++y)
		{
			edges(y, 0) = '\0';
			for (size_t x = 1; x < w; ++x)
			{
				int nsum = input(y - 1, x - 1);
				nsum += input(y - 1, x);
				nsum += input(y - 1, x + 1);
				nsum += input(y, x - 1);
				nsum += input(y, x + 1);
				nsum += input(y + 1, x - 1);
				nsum += input(y + 1, x);
				nsum += input(y + 1, x + 1);
				edges(y, x) = abs(input(y, x) * 8 - nsum);
			}
			edges(y, w) = '\0';
		}

		for (size_t x = 0; x <= w; ++x)
		{
			edges(0, x) = '\0';
		}
		for (size_t x = 0; x <= w; ++x)
		{
			edges(h, x) = '\0';
		}

		return edges;
	}
	ImageGray8 laplace4(ImageGray8& input)
	{
		ImageGray8 edges(input.width(), input.height());

		const auto h = edges.height() - 1;
		const auto w = edges.width() - 1;

		for (size_t y = 1; y < h; ++y)
		{
			for (size_t x = 1; x < w; ++x)
			{
				int nsum = input(y - 1, x);
				nsum += input(y, x - 1);
				nsum += input(y, x + 1);
				nsum += input(y + 1, x);
				edges(y, x) = abs(input(y, x) * 4 - nsum);
			}

			edges(y, 0) = edges(y, w) = '\0';
		}

		for (size_t x = 0; x <= w; ++x)
		{
			edges(0, x) = '\0';
			edges(h, x) = '\0';
		}

		return edges;
	}

	ImageGray8 LoG(ImageGray8& input)
	{
		ImageGray8 edges(input.width(), input.height());

		// abs(Laplace) naive version
		const auto h = edges.height() - 1;
		const auto w = edges.width() - 1;

		for (size_t y = 1; y < h; ++y)
		{
			edges(y, 0) = '\0';
			for (size_t x = 1; x < w; ++x)
			{
				int nsum = -input(y - 1, x - 1);
				nsum += 2 * input(y - 1, x);
				nsum += -input(y - 1, x + 1);
				nsum += 2 * input(y, x - 1);
				nsum += 2 * input(y, x + 1);
				nsum += -input(y + 1, x - 1);
				nsum += 2 * input(y + 1, x);
				nsum += -input(y + 1, x + 1);
				edges(y, x) = abs(input(y, x) * -4 + nsum);
			}
			edges(y, w) = '\0';
		}

		for (size_t x = 0; x <= w; ++x)
		{
			edges(0, x) = '\0';
		}
		for (size_t x = 0; x <= w; ++x)
		{
			edges(h, x) = '\0';
		}

		return edges;
	}

	void thresholdInPlace(ImageGray8& input, uint8_t thresholdValue)
	{
		const auto h = input.height();
		const auto w = input.width();

		for (size_t y = 0; y < h; ++y)
		{
			for (size_t x = 0; x < w; ++x)
			{
				auto p = input(y, x);
				if (p <= thresholdValue)
				{
					p = '\0';
				}
			}
		}
	}

	void thresholdInPlace(ImageGray8& input)
	{
		const auto h = input.height() - 1;
		const auto w = input.width() - 1;

		// build histogram
		constexpr size_t bins = 256;
		std::array<unsigned, bins> hist = { 0u };
		
		for (size_t y = 1; y < h; ++y)
		{
			for (size_t x = 1; x < w; ++x)
			{
				hist[input(y, x).p_]++;
			}
		}

		// ignore about 10-20% of smalles values - they're probably noise
		constexpr int cutoffEnd = 40;
		for (int i = 0; i < cutoffEnd; ++i)
		{
			hist[i] = 0u;
		}

		// try to find some threshold value
		auto avgbin = [&hist](unsigned s, unsigned e)
		{
			auto start = hist.begin() + s;
			auto end = hist.begin() + e;
			auto n = end - start;

			auto avg = std::accumulate(start, end, 0u) / n;
			auto sum = 0u;
			auto m = 0u;
			for (auto it = start; it != end; ++it, ++m)
			{
				sum += *it;
				if (sum >= avg)
				{
					return s + m;
				}
			}
			return s;
		};

		unsigned T = bins / 2;
		for (int i = 0; i < 40; ++i)
		{
			auto m1 = avgbin(0, T);
			auto m2 = avgbin(T, bins);

			auto mT = (m1 + m2) / 2;
			if (mT == T) break; // converegence point reached
			T = mT;
		}

		thresholdInPlace(input, T);
	}

	ImageRgb24 average(ColorImages& images)
	{
		const size_t w = images[0].width();
		const size_t h = images[0].height();

		ImageRgb24 result(w, h);

		Mat2D<unsigned, 3> acc(w, h);

		acc.apply<true>([](size_t x, size_t y, Mat2D<unsigned, 3>::PixelType<> p) { p.R() = p.G() = p.B() = 0; });

		for (auto& img : images)
		{
			for (size_t y = 0; y < h; ++y)
			{
				for (size_t x = 0; x < w; ++x)
				{
					auto p = img(y, x);
					auto o = acc(y, x);
					o.R() += p.R();
					o.G() += p.G();
					o.B() += p.B();
				}
			}
		}

		const float n = 1.f / images.size();
		for (size_t y = 0; y < h; ++y)
		{
			for (size_t x = 0; x < w; ++x)
			{
				auto p = acc(y, x);
				auto o = result(y, x);
				o.R() = static_cast<uint8_t>(std::min(255.f, p.R() * n));
				o.G() = static_cast<uint8_t>(std::min(255.f, p.G() * n));
				o.B() = static_cast<uint8_t>(std::min(255.f, p.B() * n));
			}
		}

		return std::move(result);
	}

	constexpr uint8_t indexToDepth(size_t i, size_t count)
	{
		return 255u - static_cast<uint8_t>(i * 255.f / static_cast<float>(count + 1));
	}

using MergeFunc = std::function<void(size_t startY, size_t endY, EdgeImages&, ColorImages&, ImageRgb24&, ImageRgb24&)>;

MergeFunc makeMergeFunc(FocusStackingImpl::MergeStrategy strategy, size_t w, uint8_t dt, ImageRgb24& avg)
{
	using MS = FocusStackingImpl::MergeStrategy;
	switch (strategy)
	{
	case MS::AlwaysMaximum:
		return [w, dt](size_t startY, size_t endY, EdgeImages& edges, ColorImages& images, ImageRgb24& out, ImageRgb24& depth)
		{
			const size_t imgCount = images.size();
			for (size_t y = startY; y < endY; ++y)
			{
				for (size_t x = 0; x < w; ++x)
				{
					uint8_t maxEdgeVal = '\0';
					size_t i = 0;
					size_t max = 0;
					for (auto& edge : edges)
					{
						auto e = edge(y, x);
						if (e > maxEdgeVal)
						{
							maxEdgeVal = e;
							max = i;
						}
						++i;
					}

					if (maxEdgeVal > dt)
					{
						auto d = depth(y, x);
						d.R() = d.G() = d.B() = indexToDepth(max, imgCount);
					}

					if (max == imgCount) --max;

					auto o = out(y, x);
					auto c = images[max](y, x);
					o.R() = c.R();
					o.G() = c.G();
					o.B() = c.B();
				}
			}
		};
	case MS::MaximumWithAverage:
	default:
		return [w, dt, &avg](size_t startY, size_t endY, EdgeImages& edges, ColorImages& images, ImageRgb24& out, ImageRgb24& depth)
		{
			ImageRgb24& avgColor = avg;
			const size_t imgCount = images.size();
			for (size_t y = startY; y < endY; ++y)
			{
				for (size_t x = 0; x < w; ++x)
				{
					uint8_t maxEdgeVal = '\0';
					size_t i = 0;
					size_t max = 0;
					for (auto& edge : edges)
					{
						auto e = edge(y, x);
						if (e > maxEdgeVal)
						{
							maxEdgeVal = e;
							max = i;
						}
						++i;
					}

					if (maxEdgeVal > dt)
					{
						auto d = depth(y, x);
						d.R() = d.G() = d.B() = indexToDepth(max, imgCount);
					}

					auto o = out(y, x);
					auto c = (max == imgCount || maxEdgeVal < dt? avgColor(y, x) : images[max](y, x));
					o.R() = c.R();
					o.G() = c.G();
					o.B() = c.B();
				}
			}
		};
	case MS::MaximumWeighted:
		return [w, dt, &avg](size_t startY, size_t endY, EdgeImages& edges, ColorImages& images, ImageRgb24& out, ImageRgb24& depth)
		{
			const size_t imgCount = images.size();
			std::vector<unsigned> weights(imgCount, 0);
			size_t i;
			for (size_t y = startY; y < endY; ++y)
			{
				for (size_t x = 0; x < w; ++x)
				{
					for (i = 0; i < imgCount; ++i)
					{
						auto e = edges[i](y, x);
						weights[i] = e;
					}
					auto totalWeights = std::accumulate(weights.begin(), weights.end(), 0.f);
					const float alpha = 1.f / totalWeights;

					float r = 0.f, g = 0.f, b = 0.f;
					float avgIndex = 0;
					for (i = 0; i < imgCount; ++i)
					{
						auto c = images[i](y, x);
						auto beta = weights[i] * alpha;
						r += beta * c.R();
						g += beta * c.G();
						b += beta * c.B();
						avgIndex += i * beta;
					}

					if (totalWeights < dt)
					{
						avgIndex = static_cast<float>(imgCount);
						auto a = avg(y, x);
						r = a.R();
						g = a.G();
						b = a.B();
					}

					auto o = out(y, x);
					o.R() = static_cast<uint8_t>(r);
					o.G() = static_cast<uint8_t>(g);
					o.B() = static_cast<uint8_t>(b);

					auto d = depth(y, x);
					d.R() = d.G() = d.B() = indexToDepth(static_cast<size_t>(std::roundf(avgIndex)), imgCount);
				}
			}
		};
	}
}

void FocusStackingImpl::merge(RawImageData outputImage, RawImageData depthImage)
{
	reportProgress_(5);

	// generate functions basing on configuration
	using GrayFunc = std::function<void(ImageGray8&)>;
	using GrayToGrayFunc = std::function<ImageGray8(ImageGray8&)>;

	GrayFunc threshold;
	GrayToGrayFunc edgeDetection;

	if (useAutomaticThreshold_)
	{
		threshold = [](ImageGray8& img) { thresholdInPlace(img); };
	}
	else if (thresholdValue_ != 0)
	{
		threshold = [this](ImageGray8&img) { thresholdInPlace(img, this->thresholdValue_); };
	}
	else
	{
		threshold = [](ImageGray8&) {};
	}

	switch (edgeMethod_)
	{
	case EdgeDetectionMethod::Laplace4:
		edgeDetection = laplace4;
		break;
	case EdgeDetectionMethod::Laplace8:
		edgeDetection = laplace8;
		break;
	case EdgeDetectionMethod::LoG:
		edgeDetection = LoG;
		break;
	case EdgeDetectionMethod::Sobel:
		edgeDetection = sobel;
		break;
	}

	const size_t w = outputImage.width_;
	const size_t h = outputImage.height_;
	const auto dt  = depthThresholdValue_;

	ColorImages images;
	images.reserve(layers_.size());

	for (auto img : layers_)
	{
		images.emplace_back(convertToMat2D<uint8_t, 3>(img));
	}

	EdgeImages edges(images.size());

	ThreadPool thp(8);

	std::atomic<size_t> imgsDone = 0;
	for (size_t i = 0; i < edges.size(); ++i)
	{
		thp.enqueue([i, &images, &edges, &imgsDone, edgeDetection, threshold, this]()
		{
			auto& img = images[i];
			auto e = edgeDetection(toGrayscale(img));
			threshold(e);
			edges[i] = std::move(e);
			this->reportProgress_(static_cast<int>(++imgsDone * 30.0f / edges.size()));
		});
	}
	thp.waitFor(edges.size());

	ImageRgb24 output = convertToMat2D<uint8_t, 3>(outputImage);
	ImageRgb24 depth = convertToMat2D<uint8_t, 3>(depthImage);

	MergeFunc mergeFunc;
	ImageRgb24 avgImage;
	if (mergeStrategy_ != MergeStrategy::AlwaysMaximum)
	{
		avgImage = std::move(average(images));
	}

	mergeFunc = makeMergeFunc(mergeStrategy_, w, dt, avgImage);
	
	std::atomic<size_t> linesDone = 0;
	for (size_t line = 0; line < h; ++line)
	{
		thp.enqueue([h, line, mergeFunc, &edges, &images, &output, &depth, &linesDone, this]()
		{
			mergeFunc(line, line + 1, edges, images, output, depth);
			this->reportProgress_(static_cast<int>(30.f + ++linesDone * 70.0f/ h));
		});
	}
	thp.waitFor(edges.size() + h);
}

void FocusStackingImpl::setReporter(ProgressReport reportCallback) 
{
	static std::mutex mutex;
	reportProgress_ = [reportCallback, this](int p) 
	{
		std::unique_lock<std::mutex> lock(mutex);  
		reportCallback(p); 
	}; 
}

void FocusStackingImpl::addInputImage(RawImageData rid)
{
	layers_.emplace_back(rid);
}
