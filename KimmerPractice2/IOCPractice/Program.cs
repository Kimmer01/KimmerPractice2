using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata.Ecma335;

namespace IOCPractice
{
    internal class Program
    {
        /// <summary>
        /// 基本概念：
        /// 1. 服务 service，是一个对象
        /// 2. 注册服务
        /// 3. 服务容器：负责管理注册的服务
        /// 4. 查询服务：创建对象及关联对象
        /// 5. 对象生命周期：Transient(瞬态)/Scoped(范围)/Singleton(单例)
        /// 
        /// DI: 降低模块之间的耦合度
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            //services.AddSingleton(typeof(ITestService), typeof(TestServiceImp1));
            //services.AddSingleton<ITestService, TestServiceImp1>();
            services.AddScoped<ITestService, TestServiceImp1>();
            services.AddTransient<ITestService, TestServiceImp2>();
            //services.AddTransient<ITestService, TestServiceImp1>();

            services.AddScoped<Dependancy2, Dependancy2>();
            services.AddScoped<Dependancy, Dependancy>();
            Func<IServiceProvider, Practice> implementationFactory = (p) => new Practice("Kimmer", p.GetService(typeof(Dependancy)) as Dependancy);
            services.AddScoped(typeof(Practice), implementationFactory);

            ITestService t;
            using (ServiceProvider sp = services.BuildServiceProvider())
            {
                ITestService t1 = sp.GetService(typeof(ITestService)) as ITestService;
                t1.Name = "Test1";
                t1.SayHi();

                ITestService t2 = sp.GetService<ITestService>();
                t1.Name = "Test1";
                t1.SayHi();
                Console.WriteLine(t1 == t2);
                t = t1;

                Dependancy dependancy = sp.GetService(typeof(Dependancy)) as Dependancy;
                dependancy.Test();

                Practice practice = sp.GetService(typeof(Practice)) as Practice;
                practice.Test();
            }

            using (ServiceProvider sp2 = services.BuildServiceProvider())
            {
                ITestService t1 = (ITestService)sp2.GetService(typeof(ITestService));
                t1.Name = "Test2";
                t1.SayHi();

                ITestService t2 = sp2.GetRequiredService<ITestService>();
                t1.Name = "Test2";
                t1.SayHi();
                Console.WriteLine(t1 == t2);
                Console.WriteLine(t1 == t);
            }

            using (ServiceProvider sp3 = services.BuildServiceProvider())
            {
                IEnumerable<ITestService> ts = sp3.GetServices<ITestService>();
                foreach (ITestService ti in ts)
                {
                    Console.WriteLine(ti.GetType());
                }
            }

            Console.ReadLine();
        }
    }
}