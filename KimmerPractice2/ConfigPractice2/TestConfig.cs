using Microsoft.Extensions.Options;

namespace ConfigPractice2;

internal class TestConfig
{
    private IOptionsSnapshot<WebConfig> _webConfig;

    public TestConfig(IOptionsSnapshot<WebConfig> webConfig)
    {
        _webConfig = webConfig;
    }

    public void Test()
    {
        Console.WriteLine(_webConfig.Value.connextionStr1.ConnectionString);
        Console.WriteLine(_webConfig.Value.connextionStr1.Provider);
        Console.WriteLine(_webConfig.Value.connextionStr2.ConnectionString);
        Console.WriteLine(_webConfig.Value.connextionStr2.Provider);
        Console.WriteLine(_webConfig.Value.Config.Name);
        //Console.WriteLine(string.Join(" ", _webConfig.Value.Config.Proxy.Ids));
    }
}
