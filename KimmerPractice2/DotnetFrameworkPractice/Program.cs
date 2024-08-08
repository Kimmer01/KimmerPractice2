using DotnetStandard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetFrameworkPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(typeof(FileStream).Assembly.Location);

            FileStreamPeactice practice = new FileStreamPeactice();
            Console.WriteLine(practice.GetType().Assembly.Location);

            practice.WriteTypeLocation();

            Console.WriteLine(typeof(FileStreamPeactice).Assembly.Location);

            Console.ReadLine();
        }
    }
}
