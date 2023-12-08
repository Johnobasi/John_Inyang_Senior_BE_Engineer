# Answers to Technical Questions

## 1. Time Spent on the Software Engineering Test

I spent approximately 2.5 hours on the Software Engineering Test.

## 2. Additional Features with More Time

If I had more time, I would enhance the solution by incorporating the following features:

- **Logging and Error Handling:** Implement a robust logging system to capture information about the order matching process, making it easier to trace issues in production.

- **Input Validation:** Add thorough input validation to ensure the submitted orders meet expected criteria, preventing potential errors and improving system reliability.

- **More Extensive Testing:** While I've included basic test cases, with additional time, I would expand the test suite to cover edge cases and handle a wider variety of scenarios.

- **Web Interface:** Develop a simple web interface to visualize the order book state dynamically, making it user-friendly for non-technical stakeholders.

## 3. Most Useful Feature in C# 9.0

### a. Feature: Record Types

One of the significant features introduced in C# 9.0 was the "record" type, which simplifies the creation of immutable classes for holding data. 
Records automatically implement value equality, immutability, and other useful methods, making them concise and expressive.

Here's an example of how i use a record type in this project:

### b. Code Snippet:

```C#

public record Order(string CompanyId, string OrderId, Direction Direction, int Volume, double Notional, string OrderDatetime)
{
    // Additional properties or methods can be added if needed
}
```
## 4. Tracking Down a Performance Issue in Production

### a. Tracking Process:

To track down a performance issue in production, I would follow these steps:

- **Code Reviews:** I conduct code reviews to ensure that best practices are followed. Inefficient algorithms, improper use of data structures, or poorly optimized code can lead to performance issues.

- **Database Queries:** I usually review and optimize database queries. Slow database queries can significantly impact application performance.
Ensure that indexes are properly configured and that queries are optimized..

- **Monitoring and Logging:** I implement robust logging and monitoring in application. I use tools like Application Performance Management (APM) systems, logging frameworks, and monitoring solutions to provide valuable insights.
Log key metrics, such as response times, error rates, and resource usage, to analyze and identify potential bottlenecks.

- **Concurrency Issues:** I check for concurrency issues, such as contention on locks or resource sharing problems.
I utilize threading and concurrency profiling tools to identify bottlenecks related to parallel execution.

- **Analyzing Stack Traces** Analyze stack traces from exceptions or performance snapshots to identify bottlenecks.
I look for areas in the code that consume a disproportionate amount of resources.

- **Memory Analysis:** I use memory analysis tools to identify memory-related issues. Profilers often include memory analysis features.
Analyze the memory usage of my application over time to identify memory leaks or inefficient memory utilization.

### b. Experience:

Yes, I have experience tracking down performance issues in production. In a previous project, we encountered a slowdown in response times during peak usage. By leveraging monitoring tools, profiling the code, and analyzing logs, we identified and optimized resource-intensive sections. This holistic approach resulted in a substantial improvement in overall system performance.
