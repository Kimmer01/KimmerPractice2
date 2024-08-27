using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCProject1Practice.ConfigService.Implementation;

internal class EnvironmentImplementation : IConfigService
{
    public string GetConfig(string key)
    {
        if (TryGetConfig(key, out string result))
        {
            return result;
        }

        throw new ArgumentException($"No {key} info in the config.");
    }

    public bool TryGetConfig(string key, out string value)
    {
        value = null;

        if (key == null)
        {
            return false;
        }

        value = Environment.GetEnvironmentVariable(key);
        return value != null;
    }
}
