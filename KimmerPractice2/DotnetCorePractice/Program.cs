using DotnetStandard;

namespace DotnetCorePractice
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region - .net standard & .net core & .net framework
            //Console.WriteLine(typeof(FileStream).Assembly.Location);

            //FileStreamPeactice practice = new FileStreamPeactice();
            //Console.WriteLine(practice.GetType().Assembly.Location);

            //practice.WriteTypeLocation();

            //Console.WriteLine(typeof(FileStreamPeactice).Assembly.Location);

            //Console.ReadLine();
            #endregion

            string fileName = @"C:\Users\kimmer_wang\Downloads\KimmerTest.txt";

            //File.WriteAllText(fileName, "Hello");
            //string s = File.ReadAllText(fileName);

            File.WriteAllTextAsync(fileName, "Hello1234");
            File.ReadAllTextAsync(fileName);

            await File.WriteAllTextAsync(fileName, "Hello");
            string s = await File.ReadAllTextAsync(fileName);

            Console.WriteLine(s);

            Console.ReadLine();
        }
    }
}