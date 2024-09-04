using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigPractice2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.Add(new KimConfihurationSource() { Path = "WebConfig.xml" });
            builder.AddUserSecrets<Program>();
            IConfigurationRoot configRoot = builder.Build();

            ServiceCollection services = new ServiceCollection();
            services.AddScoped<TestConfig>();
            services.AddOptions()
                .Configure<WebConfig>(e => configRoot.Bind(e));

            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                TestConfig c = sp.GetService<TestConfig>();
                c.Test();
            }
        }
    }
}