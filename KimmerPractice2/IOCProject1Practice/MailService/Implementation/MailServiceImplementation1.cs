using IOCProject1Practice.ConfigService;
using IOCProject1Practice.LogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCProject1Practice.MailService.Implementation;

internal class MailServiceImplementation1 : IMailService
{
    private readonly IConfigService _config;
    private readonly ILogService _log;

    public MailServiceImplementation1(IConfigService config, ILogService log)
    {
        _config = config;
        _log = log;
    }

    public void SendEmail(string to, string subject, string body)
    {
        _log.LogInfo("Start to send email.");

        string from = _config.GetConfig("from");

        Console.WriteLine($"Email send. from: {from}, to: {to}, subject: {subject}, body: {body}");
        _log.LogInfo("End to send email.");
    }
}
