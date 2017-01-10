#pragma once

#include <cstdint>
#include "RawImageData.h"

template <class T, size_t Channels, bool Invert = false>
struct Pixel
{
	Pixel(T& pixelStart) : p_(pixelStart) {}

	template<typename = std::enable_if<(Channels > 0)>::type>
	T& R() { return *(&p_ + offset<0, Channels, Invert>()); }

	template<typename = std::enable_if<(Channels > 1)>::type>
	T& G() { return *(&p_ + offset<1, Channels, Invert>()); }

	template<typename = std::enable_if<(Channels > 2)>::type>
	T& B() { return *(&p_ + offset<2, Channels, Invert>()); }

	template<typename = std::enable_if<(Channels > 3)>::type>
	T& A() { return *(&p_ + offset<3, Channels, false>()); }

	T& operator=(T v)
	{
		p_ = v;
		return p_;
	}

	operator T()
	{
		return p_;
	}

	T& p_;
private:
	template<int I, int Channels, bool Invert = false>
	static size_t offset()
	{
		static_assert(I < Channels, "index cannot be greater than number of channels");
		return Invert ? Channels - 1 - I : I;
	}
};

// Low-level 2D image matrix
template <class T, size_t Channels = 1>
class Mat2D
{
public:
	Mat2D() : data_(nullptr), width_(0), height_(0), stride_(0), freeMem_(Mat2D::dontFree)
	{

	}

	// Allocate Image
	Mat2D(size_t width, size_t height)
		: width_(width), height_(height), stride_(width * Channels), freeMem_(Mat2D::free)
	{
		data_ = new T[stride_ * height_];
	}

	// Create view-only
	Mat2D(T* data, size_t width, size_t height, size_t stride)
		: data_(data), width_(width), height_(height), stride_(stride), freeMem_(Mat2D::dontFree)
	{

	}

	Mat2D(Mat2D&& other)
		: width_(other.width_), height_(other.height_), stride_(other.stride_)
	{
		data_ = other.data_;
		other.data_ = nullptr;

		freeMem_ = other.freeMem_;
		other.freeMem_ = Mat2D::dontFree;
	}

	// as this is not cheap operation, coder should explictly call clone()
	Mat2D(Mat2D<T, Channels>& other) = delete;

	void operator=(Mat2D&& other)
	{
		width_  = other.width_;
		height_ = other.height_;
		stride_ = other.stride_;

		data_ = other.data_;
		other.data_ = nullptr;

		freeMem_ = other.freeMem_;
		other.freeMem_ = Mat2D::dontFree;
	}

	~Mat2D()
	{
		freeMem_(data_);
	}

	size_t width()  const { return width_; }
	size_t height() const { return height_; }
	size_t stride() const { return stride_; }
	static size_t channels() { return Channels; }

	void* raw() const { return data_; }

	template <bool BGROrder = true>
	using PixelType = Pixel<T, Channels, BGROrder>;

	T& at(size_t j, size_t i, size_t channel = 0)
	{
		return data_[(i*Channels + stride_ * j) + channel];
	}

	template <bool BGROrder = true>
	Pixel<T, Channels, BGROrder> operator()(size_t j, size_t i)
	{
		return pixel(j, i);
	}

	template <bool BGROrder = true>
	Pixel<T, Channels, BGROrder> pixel(size_t j, size_t i)
	{
		return Pixel<T, Channels, BGROrder>(at(j, i, 0));
	}

	template <bool BGROrder = true>
	Mat2D& apply(std::function<void(size_t, size_t, PixelType<BGROrder>)> function)
	{
		for (size_t y = 0; y < height_; ++y)
		{
			for (size_t x = 0; x < width_; ++x)
			{
				function(x, y, pixel(y, x));
			}
		}
		return *this;
	}

private:
	T*     data_;
	size_t width_;
	size_t height_;
	size_t stride_;
	void(*freeMem_)(T*);
	static void free(T* data) { delete[] data; data = nullptr; }
	static void dontFree(T* data) { }
};


// Handy typedefs
using ImageRgb24 = Mat2D<uint8_t, 3>;
using ImageGray8 = Mat2D<uint8_t, 1>;
using ImageGrayFloat = Mat2D<float, 1>;

template <class T, size_t Channels>
Mat2D<T, Channels> convertToMat2D(RawImageData img)
{
	Mat2D<T, Channels> out(reinterpret_cast<T*>(img.data_), img.width_, img.height_, img.stride_);
	return std::move(out);
}
