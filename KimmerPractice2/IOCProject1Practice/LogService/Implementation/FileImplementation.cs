using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCProject1Practice.LogService.Implementation;

internal class FileImplementation : ILogService
{
    private readonly string _fileName;

    public FileImplementation(string fileName = null)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = Path.Combine(Environment.CurrentDirectory, "log.txt");
        }

        _fileName = fileName;
    }

    public void LogError(string message, Exception ex)
    {
        File.AppendAllTextAsync(_fileName, $"Error: {message}. {ex.InnerException}");
    }

    public void LogInfo(string message)
    {
        File.AppendAllTextAsync(_fileName, $"Info: {message}");
    }

    public void LogWarning(string message)
    {
        File.AppendAllTextAsync(_fileName, $"Warning: {message}");
    }
}
