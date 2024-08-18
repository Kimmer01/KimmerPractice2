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

            //Console.WriteLine($"Before threadId: {Thread.CurrentThread.ManagedThreadId}");
            //await TestAwaitThreadAsync(5000);
            //Console.WriteLine($"After threadId: {Thread.CurrentThread.ManagedThreadId}");

            string str = await TestAsyncMethodWithoutAsyncKeyWordAsync(2);
            Console.WriteLine(str);
        }

        /// <summary>
        /// 1. async await 是 语法糖，最终编译成 ‘状态机调用’
        /// <para> 
        /// async 的方法，会被 C# 编译器编译成一个类，
        /// 主要根据 await调用 进行切分成多个状态，
        /// 对 async 方法的调用会被拆分为对 MoveNext 的调用。
        /// </para>
        /// 2. 异步方法未必一定使用多个线程，也有可能没切换线程(线程还没来的及切换的时候，await 已经执行完了，就不会切换线程)
        /// 3. async 方法缺点
        /// <para>
        /// async 方法会被编译成一个类，运行效率没有普通方法高
        /// 可能会占用非常多的线程
        /// </para>
        /// 4. 如果想在 async 方法中等待一段时间，不要使用 Thread.Sleep()， 因为会阻塞当前线程；使用 await Task.Delay()
        /// </summary>
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

        /// <summary>
        /// 直接返回 Task，不拆完了再装，只是普通的方法调用，没有像 async 方法编译成一个类
        /// 运行效率更高，不会造成线程浪费
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        static Task<string> TestAsyncMethodWithoutAsyncKeyWordAsync(int num)
        {
            switch (num)
            {
                case 1:
                    return File.ReadAllTextAsync(@"C:\Users\kimmer_wang\Downloads\KimmerTest1.txt");
                case 2:
                    return File.ReadAllTextAsync(@"C:\Users\kimmer_wang\Downloads\KimmerTest2.txt");
                default:
                    throw new ArgumentException();
            }
        }


        static Task<double> TestAwaitThreadWithoutAsyncKeyWordAsync(int times)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"{nameof(TestAwaitThreadAsync)} threadId: {Thread.CurrentThread.ManagedThreadId}");
                double result = 0;
                Random rand = new Random();
                for (int n = 0; n < times; n++)
                {
                    result += rand.NextDouble();
                }

                return Task.FromResult(result);
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