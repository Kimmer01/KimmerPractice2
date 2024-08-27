using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCProject1Practice.LogService.Implementation;

internal class ConsoleImplementation : ILogService
{
    public void LogError(string message, Exception ex)
    {
        Console.WriteLine($"Error: {message}. {ex.InnerException}");
    }

    public void LogInfo(string message)
    {
        Console.WriteLine($"Info: {message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine($"Warning: {message}");
    }
}
