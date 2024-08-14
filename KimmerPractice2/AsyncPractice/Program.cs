namespace AsyncPractice
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region - Async simple practice
            //string fileName = @"C:\Users\kimmer_wang\Downloads\KimmerTest.txt";

            ////File.WriteAllText(fileName, "Hello");
            ////string s = File.ReadAllText(fileName);

            //File.WriteAllTextAsync(fileName, "Hello1234");
            //File.ReadAllTextAsync(fileName);

            //await File.WriteAllTextAsync(fileName, "Hello");
            //string s = await File.ReadAllTextAsync(fileName);

            //Console.WriteLine(s);

            //Console.ReadLine();
            #endregion

            await DownloadHtmlToFile(@"https://www.baidu.com/",
                @"C:\Users\kimmer_wang\Downloads\KimmerTest.txt");

            int length = await DownloadHtmlToFileAndGetContentLength(@"https://www.baidu.com/",
                @"C:\Users\kimmer_wang\Downloads\KimmerTest.txt");
            Console.WriteLine(length);
        }

        static async Task DownloadHtmlToFile(string url, string fileName)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(url);

                await File.WriteAllTextAsync(fileName, content);
            }
        }

        static async Task<int> DownloadHtmlToFileAndGetContentLength(string url, string fileName)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(url);

                await File.WriteAllTextAsync(fileName, content);

                return content.Length;
            }
        }
    }
}