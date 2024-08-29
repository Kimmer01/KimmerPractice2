using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks; 

namespace IOCProject1Practice.ConfigService.Implementation;

internal class FileImplementation : IConfigService
{
    private readonly Dictionary<string, string> _configs;

    public FileImplementation(string fileName = null)
    {
        if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
        {
            fileName = Path.Combine(Environment.CurrentDirectory, "appSettings.json");
        }

        string content = File.ReadAllText(fileName);

        _configs = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
    }

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
