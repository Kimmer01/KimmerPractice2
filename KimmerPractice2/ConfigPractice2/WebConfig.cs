using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigPractice2;

internal class WebConfig
{
    public Connection connextionStr1 { get; set; }

    public Connection connextionStr2 { get; set; }

    public Config Config { get; set; }
}

class Connection
{
    public string ConnectionString { get; set; }

    public string Provider { get; set; }
}
