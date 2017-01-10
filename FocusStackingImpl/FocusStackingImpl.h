#pragma once

#include <vector>
#include <functional>
#include "RawImageData.h"

class FocusStackingImpl
{
public:
	enum class EdgeDetectionMethod
	{
		Laplace4,
		Laplace8,
		LoG,
		Sobel
	};

	enum class MergeStrategy
	{
		AlwaysMaximum,
		MaximumWithAverage,
		MaximumWeighted
	};

	using ProgressReport = void(*)(int);

	FocusStackingImpl(size_t layersCountHint = 2);
	~FocusStackingImpl();

	void merge(RawImageData outputImage, RawImageData depthImage);

	void setReporter(ProgressReport reportCallback);

	void setManualEdgeThreshold(uint8_t threshold) { thresholdValue_ = threshold; useAutomaticThreshold_ = false; }
	void useAutomaticThreshold() { useAutomaticThreshold_ = true; }
	void setEdgeDetectionMethod(EdgeDetectionMethod method) { edgeMethod_ = method; }
	void setMergeStrategy(MergeStrategy strategy) { mergeStrategy_ = strategy; }
	void setDepthThreshold(uint8_t threshold) { depthThresholdValue_ = threshold; }

	void addInputImage(RawImageData rid);
	void clearInputImages();
private:
	static void noReporting(int) { }

	std::vector<RawImageData> layers_;
	uint8_t                   thresholdValue_;
	uint8_t                   depthThresholdValue_;
	bool                      useAutomaticThreshold_;
	EdgeDetectionMethod       edgeMethod_;
	MergeStrategy             mergeStrategy_;
	std::function<void(int)>  reportProgress_;
};
