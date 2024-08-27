using IOCProject1Practice.ConfigService;
using IOCProject1Practice.ConfigService.Implementation;
using IOCProject1Practice.LogService;
using IOCProject1Practice.MailService;
using IOCProject1Practice.MailService.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication.ExtendedProtection;

namespace IOCProject1Practice;

internal class Program
{
    static void Main(string[] args)
    {
        ServiceCollection services = new ServiceCollection();
        services.AddSingleton<IConfigService, ConfigService.Implementation.FileImplementation>();
        services.AddSingleton<ILogService, LogService.Implementation.FileImplementation>();
        services.AddSingleton<IMailService, MailServiceImplementation1>();

        using (ServiceProvider sp = services.BuildServiceProvider())
        {
            IMailService mail = sp.GetService<IMailService>();

            mail.SendEmail("toTest", "subjectTest", "bodyTest");
        }
    }
}