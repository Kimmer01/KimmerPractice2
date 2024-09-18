using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogPractice;

internal class Test
{
    private readonly ILogger<Test> _logger;

    public Test(ILogger<Test> logger)
    {
        _logger = logger;
    }

    public void TestFunc()
    {
        try
        {
            throw new Exception("Kimmer Test");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Test");
        }
    }
}
