using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCProject1Practice.ConfigService;

internal interface IConfigService
{
    string GetConfig(string key);

    bool TryGetConfig(string key, out string value);
}
