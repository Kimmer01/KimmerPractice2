﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCProject1Practice.MailService;

internal interface IMailService
{
    void SendEmail(string to, string subject, string body);
}
