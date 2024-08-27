using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCProject1Practice.ConfigService.Implementation;

internal class ConfigImplementation1 : IConfigService
{
    private readonly Dictionary<string, string> _configs = new Dictionary<string, string>() {
        { "ado","connectionStr"}
    };

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
        return _configs.TryGetValue(key, out value);
    }
}
