#pragma once

struct RawImageData
{
	RawImageData(void* data, size_t width, size_t height, size_t stride)
		: data_(data), width_(width), height_(height), stride_(stride)
	{
	}

	void*  data_;
	size_t width_;
	size_t height_;
	size_t stride_;
};

