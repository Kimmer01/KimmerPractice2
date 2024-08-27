using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCProject1Practice.LogService;

internal interface ILogService
{
    void LogInfo(string message);

    void LogError(string message, Exception ex);

    void LogWarning(string message);
}
