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

            #region - TestAwaitThreadAsync
            //Console.WriteLine($"Before threadId: {Thread.CurrentThread.ManagedThreadId}");

            //CancellationTokenSource cts = new CancellationTokenSource();
            //cts.CancelAfter(10);
            //await TestAwaitThreadAsync(500000, cts.Token);
            //Console.WriteLine($"After threadId: {Thread.CurrentThread.ManagedThreadId}");
            #endregion

            //string str = await TestAsyncMethodWithoutAsyncKeyWordAsync(2);
            //Console.WriteLine(str);

            //Task<string> t1 = File.ReadAllTextAsync(@"C:\Users\kimmer_wang\Downloads\KimmerTest1.txt");
            //Task<string> t2 = File.ReadAllTextAsync(@"C:\Users\kimmer_wang\Downloads\KimmerTest2.txt");

            //string[] result = await Task.WhenAll(t1, t2);
            //Console.WriteLine(result[0]);
            //Console.WriteLine(result[1]);

            int count = await CountFilesStringLengthAsync(@"C:\Users\kimmer_wang\Downloads\New folder\PdfService");
            Console.WriteLine(count);
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
        /// 5. .net core 程序的 action 都可以加一个 CancellationToken 参数，当用户关闭当前网页时，会 cancel，可以节约资源 TODO: 如何应用到PIEX项目
        /// 6. Task.WhenAll & Task.WhenAny
        /// 7. async 试提示编译器为异步代码中的 await 代码进行分段处理的，而一个异步方法是否修饰了 async 对于方法的调用者来说是没区别的。因此对于接口中的方法或者抽象方法不能修饰为 async，将返回值标为 Task 即可
        /// 8. yield return(也是编译成一个类) 能够简化数据的返回，而且可以让数据处理‘流水线化’，提升性能
        /// <para>
        /// IAsyncEnumerable
        /// </para>
        /// </summary>
        static async Task<double> TestAwaitThreadAsync(int times, CancellationToken cancellationToken)
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

                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine($"The task was cancelled. n = {n}");
                        break;
                    }
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

        static async Task<int> CountFilesStringLengthAsync(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            Task<int>[] lengthTasks = new Task<int>[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                lengthTasks[i] = CountFileStringLengthAsync(file);
            }
            
            int[] lengths = await Task.WhenAll(lengthTasks);
            int count = lengths.Sum();

            return count;
        }

        static async Task<int> CountFileStringLengthAsync(string fileName)
        {
            string content = await File.ReadAllTextAsync(fileName);
            return content.Length;
        }


        static IEnumerable<int> test10() {
            for (int i = 0; i < 10; i++)
            {
                yield return i;
            }
        }
    }
}