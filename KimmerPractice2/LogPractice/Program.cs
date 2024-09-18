using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LogPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddLogging(logBuilder => logBuilder.AddEventLog());
            services.AddSingleton<Test>();

            ServiceProvider sp = services.BuildServiceProvider();

            Test test = sp.GetService<Test>();

            test.TestFunc();
        }
    }
}