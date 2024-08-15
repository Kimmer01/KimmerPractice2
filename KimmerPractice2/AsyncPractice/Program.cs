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

            #region - async & await & .Wait() & .Result
            //DownloadHtmlToFile(@"https://www.baidu.com/", @"C:\Users\kimmer_wang\Downloads\KimmerTest.txt");

            //await DownloadHtmlToFileAsync(@"https://www.baidu.com/",
            //    @"C:\Users\kimmer_wang\Downloads\KimmerTest.txt");

            //int length = await DownloadHtmlToFileAndGetContentLengthAsync(@"https://www.baidu.com/",
            //    @"C:\Users\kimmer_wang\Downloads\KimmerTest.txt");
            //Console.WriteLine(length);
            #endregion

            //await TestAwaitThreadAsync(@"https://www.baidu.com/", @"C:\Users\kimmer_wang\Downloads\KimmerTest.txt","Test");

            Console.WriteLine($"Before threadId: {Thread.CurrentThread.ManagedThreadId}");
            await TestAwaitThreadAsync(5000);
            Console.WriteLine($"After threadId: {Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task<double> TestAwaitThreadAsync(int times)
        {
            //Console.WriteLine($"{nameof(TestAwaitThreadAsync)} threadId: {Thread.CurrentThread.ManagedThreadId}");
            //double result = 0;
            //Random rand = new Random();
            //for (int n = 0; n < times; n++)
            //{
            //    result += rand.NextDouble();
            //}

            //return result;

            return await Task.Run(() =>
            {
                Console.WriteLine($"{nameof(TestAwaitThreadAsync)} threadId: {Thread.CurrentThread.ManagedThreadId}");
                double result = 0;
                Random rand = new Random();
                for (int n = 0; n < times; n++)
                {
                    result += rand.NextDouble();
                }

                return result;
            });
        }

        static async Task<string> TestAwaitThreadAsync(string url, string fileName, string fileContent)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(url);
            }

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            await File.WriteAllTextAsync(fileName, fileContent);

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            string fileContent2 = await File.ReadAllTextAsync(fileName);

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            return fileContent2;
        }

        /// <summary>
        /// If need call async method inside a non-async method, 
        /// use .Wait() if async method no return value
        /// use .Result if async method have return value
        /// </summary>
        static void DownloadHtmlToFile(string url, string fileName)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = client.GetStringAsync(url).Result;

                File.WriteAllTextAsync(fileName, content).Wait();
            }
        }

        static async Task DownloadHtmlToFileAsync(string url, string fileName)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(url);

                await File.WriteAllTextAsync(fileName, content);
            }
        }

        static async Task<int> DownloadHtmlToFileAndGetContentLengthAsync(string url, string fileName)
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