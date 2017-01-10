#pragma once

public ref class Config
{
public:
	enum class EdgeDetectionMethodEnum
	{
		Laplace4,
		Laplace8,
		LoG,
		Sobel
	};

	enum class MergeStrategyEnum
	{
		AlwaysMaximum,
		MaximumWithAverage,
		MaximumWeighted
	};

	property MergeStrategyEnum MergeStrategy;
	property EdgeDetectionMethodEnum EdgeDetMethod;
	property int EdgeThreshold;
	property int DepthThreshold;
};