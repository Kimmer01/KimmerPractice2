namespace AsyncPractice
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
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