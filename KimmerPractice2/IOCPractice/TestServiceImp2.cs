using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCPractice;

internal class TestServiceImp2: ITestService
{
    public string Name { get; set; }

    public void SayHi()
    {
        Console.WriteLine($"{Name} in {nameof(TestServiceImp2)} say hello!");
    }
}
