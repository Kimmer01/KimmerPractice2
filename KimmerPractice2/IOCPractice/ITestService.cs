using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCPractice;

internal interface ITestService
{
    public string Name { get; set; }

    void SayHi();
}
