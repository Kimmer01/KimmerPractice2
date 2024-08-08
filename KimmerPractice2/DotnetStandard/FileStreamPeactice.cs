using System;
using System.IO;

namespace DotnetStandard
{
    public class FileStreamPeactice
    {
        public Type GetType()
        {
            return typeof(FileStream);
        }

        public void WriteTypeLocation()
        {
            Console.WriteLine(typeof(FileStream).Assembly.Location);
        }
    }
}
