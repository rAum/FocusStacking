#pragma once

#include "ThreadPool.h"
#include <chrono>

ThreadPool::ThreadPool(size_t size)
	: stop_(false), tasksDone_(0)
{
	for (size_t i = 0u; i < size; ++i)
	{
		workers_.emplace_back(std::bind(&ThreadPool::worker, this));
	}
}

ThreadPool::~ThreadPool()
{
	stop_ = true;
	wakeup_.notify_all();
	for (auto& worker : workers_)
		worker.join();
}

void ThreadPool::worker()
{
	Task task;
	while(true)
	{
		{
			std::unique_lock<std::mutex> lock(qMutex_);

			while (!stop_ && tasks_.empty())
			{
				wakeup_.wait(lock);
			}

			if (stop_) return;

			task = tasks_.front();
			tasks_.pop();
		}

		if (task)
		{
			task();
			++tasksDone_;
		}
		task = nullptr;
	}

}

void ThreadPool::enqueue(Task task)
{
	{
		std::unique_lock<std::mutex> lock(qMutex_);
		tasks_.push(task);
	}
	wakeup_.notify_one();
}

void ThreadPool::waitFor(size_t tasksNum)
{
	while (tasksDone_ < tasksNum)
	{
		using namespace std::chrono_literals;
		std::this_thread::sleep_for(5ms);
	}
}