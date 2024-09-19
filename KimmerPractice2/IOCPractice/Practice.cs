using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCPractice;

internal class Practice
{
    public Practice(string input, Dependancy dependancy)
    {
        Input = input;
        Dependancy = dependancy;
    }

    public string Input { get; }
    public Dependancy Dependancy { get; }

    public void Test()
    {
        Console.WriteLine(Input);
    }
}

internal class Dependancy
{
    public Dependancy(Dependancy2 dependancy2)
    {
        Dependancy2 = dependancy2;
    }

    public Dependancy2 Dependancy2 { get; }

    public void Test()
    {
        Console.WriteLine(typeof(Dependancy));
    }
}

internal class Dependancy2
{
    public Dependancy2()
    {

    }
}