#pragma once

#include <vector>
#include <queue>
#include <functional>
#include <thread>
#include <mutex>
#include <condition_variable>
#include <atomic>


class ThreadPool
{
public:
	ThreadPool(size_t size = 4);
	~ThreadPool();

	using Task = std::function<void()>;

	void enqueue(Task task);

	void waitFor(size_t tasksNum);
private:
	void worker();

	std::vector<std::thread> workers_;
	std::mutex               qMutex_;
	std::condition_variable  wakeup_;
	std::queue<Task>         tasks_;
	std::atomic<bool>        stop_;
	std::atomic<size_t>      tasksDone_;
};