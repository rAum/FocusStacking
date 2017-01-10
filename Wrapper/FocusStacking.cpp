#include "FocusStacking.h"
#include "../FocusStackingImpl/FocusStackingImpl.h"

using namespace Imaging;
using namespace System::Runtime::InteropServices;

FocusStacking::FocusStacking() : config_(nullptr)
{
}

FocusStacking::~FocusStacking()
{
}

FocusStacking::!FocusStacking()
{
}

void FocusStacking::AddImage(Bitmap^ image)
{
	inputImages_.Add(image);
}

void FocusStacking::SetReporter(ReportProgress^ rp)
{
	progress_ = rp;
}

void FocusStacking::ValidateImages()
{
	if (inputImages_.Count < 2)
	{
		throw gcnew System::Exception("There should be at least two images.");
	}

	const int w = inputImages_[0]->Width;
	const int h = inputImages_[0]->Height;

	for each (Bitmap^ img in inputImages_)
	{
		if (img->Width != w || img->Height != h)
		{
			throw gcnew System::Exception("Images are having different size");
		}

		if (img->PixelFormat != PixelFormat::Format24bppRgb)
		{
			throw gcnew System::Exception("Images must be 24 bpp rgb");
		}
	}
}

private ref class UnpackBitmap
{
public:
	UnpackBitmap(Bitmap^ bmp) : bmp_(bmp), isLocked_(false)
	{
		Lock();
	}

	property RawImageData AsRawImageData
	{
		RawImageData get() 
		{
			return RawImageData(
				bd_->Scan0.ToPointer(), 
				bd_->Width, 
				bd_->Height, 
				Math::Abs(bd_->Stride));
		}
	}

	~UnpackBitmap() { Unlock(); }
private:
	void Unlock()
	{
		if (isLocked_)
		{
			bmp_->UnlockBits(bd_);
			isLocked_ = false;
		}
	}

	void Lock()
	{
		bd_ = bmp_->LockBits(
			Rectangle(0, 0, bmp_->Width, bmp_->Height),
			ImageLockMode::ReadWrite,
			PixelFormat::Format24bppRgb);
		isLocked_ = true;
	}

	Bitmap^     bmp_;
	BitmapData^ bd_;
	bool        isLocked_;
};

void FocusStacking::applyConfig(FocusStackingImpl& fs)
{
	if (config_ == nullptr) return;

	if (config_->DepthThreshold > 0 && config_->DepthThreshold < 255)
	{
		fs.setDepthThreshold(static_cast<uint8_t>(config_->DepthThreshold));
	}

	if (config_->EdgeThreshold > 0 && config_->EdgeThreshold < 255)
	{
		fs.setManualEdgeThreshold(static_cast<uint8_t>(config_->EdgeThreshold));
	}
	else
	{
		fs.useAutomaticThreshold();
	}

	FocusStackingImpl::EdgeDetectionMethod edgeDet;
	switch (config_->EdgeDetMethod)
	{
	case Config::EdgeDetectionMethodEnum::Laplace4:
		edgeDet = FocusStackingImpl::EdgeDetectionMethod::Laplace4;
		break;
	case Config::EdgeDetectionMethodEnum::Laplace8:
		edgeDet = FocusStackingImpl::EdgeDetectionMethod::Laplace8;
		break;
	case Config::EdgeDetectionMethodEnum::LoG:
		edgeDet = FocusStackingImpl::EdgeDetectionMethod::LoG;
		break;
	case Config::EdgeDetectionMethodEnum::Sobel:
	default:
		edgeDet = FocusStackingImpl::EdgeDetectionMethod::Sobel;
		break;
	}
	fs.setEdgeDetectionMethod(edgeDet);

	FocusStackingImpl::MergeStrategy mergeStrategy;
	switch (config_->MergeStrategy)
	{
	case Config::MergeStrategyEnum::AlwaysMaximum:
		mergeStrategy = FocusStackingImpl::MergeStrategy::AlwaysMaximum;
		break;
	case Config::MergeStrategyEnum::MaximumWithAverage:
		mergeStrategy = FocusStackingImpl::MergeStrategy::MaximumWithAverage;
		break;
	case Config::MergeStrategyEnum::MaximumWeighted:
		mergeStrategy = FocusStackingImpl::MergeStrategy::MaximumWeighted;
		break;
	}
	fs.setMergeStrategy(mergeStrategy);
}

void FocusStacking::Process()
{
	Bitmap^ img = inputImages_[0];

	outputImage_ = gcnew Bitmap(img->Width, img->Height, img->PixelFormat);
	depthMap_    = gcnew Bitmap(img->Width, img->Height, PixelFormat::Format24bppRgb);

	List<UnpackBitmap^> unpacked;
	for each (Bitmap^ img in inputImages_)
	{
		unpacked.Add(gcnew UnpackBitmap(img));
	}

	FocusStackingImpl focusStacking(unpacked.Count);
	if (progress_)
	{
		focusStacking.setReporter(reinterpret_cast<FocusStackingImpl::ProgressReport>(Marshal::GetFunctionPointerForDelegate(progress_).ToPointer()));
	}

	for each (UnpackBitmap^ img in unpacked)
	{
		focusStacking.addInputImage(img->AsRawImageData);
	}

	applyConfig(focusStacking);

	UnpackBitmap out(outputImage_);
	UnpackBitmap depth(depthMap_);
	focusStacking.merge(out.AsRawImageData, depth.AsRawImageData);
}
