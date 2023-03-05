#include <iostream>
#include <chrono>
#include <ctime>

int main()
{
    auto start = std::chrono::system_clock::now();

    std::cout << "Hello World!\n";

    auto end = std::chrono::system_clock::now();

    std::chrono::duration<double> elapsed_seconds = end - start;
    std::time_t start_time = std::chrono::system_clock::to_time_t(start);
    std::time_t end_time = std::chrono::system_clock::to_time_t(end);

    std::cout << "started computation at " << std::ctime(&start_time)
        << "finished computation at " << std::ctime(&end_time)
        << "time delta: " << elapsed_seconds.count() << " sec"
        << std::endl;
}
