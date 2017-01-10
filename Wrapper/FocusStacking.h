#pragma once

#include "Config.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Drawing;
using namespace Runtime::InteropServices;

class FocusStackingImpl;

public ref class FocusStacking
{
public:
	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate void ReportProgress(int p);

	FocusStacking();

	void AddImage(Bitmap^ image);

	void setConfig(Config ^ cfg) { config_ = cfg; }

	/// Throws if errors.
	void ValidateImages();

	void Process();

	void SetReporter(ReportProgress^ rp);

	property Bitmap^ Result
	{
		Bitmap^ get() { return outputImage_;  }
	}

	property Bitmap^ DepthMap
	{
		Bitmap^ get() { return depthMap_; }
	}

	~FocusStacking();
	!FocusStacking();
private:
	void applyConfig(FocusStackingImpl& fs);

	List<Bitmap^>   inputImages_;
	Bitmap^         outputImage_;
	Bitmap^         depthMap_;
	ReportProgress^ progress_;
	Config^         config_;
};
