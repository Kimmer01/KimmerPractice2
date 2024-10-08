﻿using DotnetStandard;

namespace DotnetCorePractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region - .net standard & .net core & .net framework
            Console.WriteLine(typeof(FileStream).Assembly.Location);

            FileStreamPeactice practice = new FileStreamPeactice();
            Console.WriteLine(practice.GetType().Assembly.Location);

            practice.WriteTypeLocation();

            Console.WriteLine(typeof(FileStreamPeactice).Assembly.Location);

            Console.ReadLine();
            #endregion
        }
    }
}