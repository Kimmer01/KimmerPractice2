using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigPractice;

internal class TestConfig
{
    private IOptionsSnapshot<Config> _config;

    public TestConfig(IOptionsSnapshot<Config> config)
    {
        _config = config;
    }

    public void Test()
    {
        Console.WriteLine(_config.Value.Age);
        Console.WriteLine(_config.Value.Proxy.Address);
    }
}
