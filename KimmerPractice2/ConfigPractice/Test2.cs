using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigPractice;

internal class Test2
{
    private IOptionsSnapshot<Proxy> _config;

    public Test2(IOptionsSnapshot<Proxy> config)
    {
        _config = config;
    }

    public void Test()
    {
        Console.WriteLine(_config.Value.Address);
    }
}
