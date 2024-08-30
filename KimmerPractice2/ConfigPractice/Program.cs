using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigPractice
{
    /// <summary>
    /// 1. IOptions<T>: 不会读取到新的值
    /// 2. IOptionsMonitor<T>: 立即读取新的值
    /// 3. IOptionsSnapshot<T>: 在同一个范围内(比如 Asp.Net Core 的一个请求中)，保持一致。建议使用 IOptionsSnapshot
    /// 在读取配置的地方，使用 IOptionsSnapshot<T> 注入。不要在构造函数里直接读取 IOptionsSnapshot.Value，而是到用到的地方再读取，否则就无法更新变化。
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigurationBuilder configBuilder = new();
            //configBuilder.AddJsonFile("config.json", optional: false, reloadOnChange: true);
            //configBuilder.AddCommandLine(args);
            configBuilder.AddEnvironmentVariables("KT_");

            IConfigurationRoot configRoot = configBuilder.Build();
            //string name = configRoot["name"];
            //string proxyAddress = configRoot.GetSection("proxy:address").Value;

            Proxy p = configRoot.GetSection("proxy").Get<Proxy>();
            Config c = configRoot.Get<Config>();

            ServiceCollection services = new ServiceCollection();
            services.AddScoped<TestConfig>();
            services.AddScoped<Test2>();

            //!!!!!!!!!!!!
            services.AddOptions()
                .Configure<Config>(e => configRoot.Bind(e))
                .Configure<Proxy>(e => configRoot.GetSection("proxy").Bind(e));

            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                while (true)
                {
                    using (IServiceScope scope = sp.CreateScope())
                    {
                        TestConfig t1 = scope.ServiceProvider.GetRequiredService<TestConfig>();
                        t1.Test();
                        Test2 t2 = scope.ServiceProvider.GetRequiredService<Test2>();
                        t2.Test();
                    }

                    Console.ReadKey();
                }
            }

            Console.ReadLine();
        }
    }
}

class Config
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Proxy Proxy { get; set; }
}

class Proxy
{
    public string Address { get; set; }

    public List<int> Ids { get; set; }
}