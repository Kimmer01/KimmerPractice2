namespace LinqPractice
{
    internal class Program
    {
        /// <summary>
        /// 1. 委托是可以指向方法的类型，调用委托变量时执行的就是变量指向的方法
        /// 2. .NET中定义了泛型委托 Action(无返回值)和 Func(有返回值)，所以一般不用自己定义委托类型
        /// 3. LINQ 中提供了很多 集合 的扩展方法，配合 lambda 能简化数据处理 -  IEnumerable
        /// <para>
        /// delegate void D1();
        /// delegate int D2(string param);
        /// </para>
        /// </summary>
        static void Main(string[] args)
        {
            Action a11 = Test11();
            a11();

            Action a12 = Test12();
            a12();

            Func<int, int, string> a21 = Test21();
            a21(1, 3);
        }


        /// <summary>
        /// 委托变量不仅可以指向普通方法，也可以指向匿名方法
        /// </summary>
        static Action Test11()
        {
            Action a = delegate () { Console.WriteLine($"{nameof(a)}"); };
            return a;
        }

        static Action Test12()
        {
            Action a = () => { Console.WriteLine($"{nameof(a)}"); };
            return a;
        }

        /// <summary>
        /// 匿名方法可以写成 lambda 表达式
        /// 可以省略参数 数据类型，因为编译能根据委托类型推断出参数的类型，用 => 引出来方法体
        /// </summary>
        /// <returns></returns>
        static Func<int, int, string> Test21()
        {
            Func<int, int, string> a = (i1, i2) =>
            {
                return $"{i1} & {i2}";
            };
            return a;
        }

        /// <summary>
        /// 如果 => 之后的方法体中只有一行代码，且无返回值，那么可以省略方法体的 {} 
        /// 如果 => 之后的方法体中只有一行代码，且有返回值，那么可以省略方法体的 {} 以及 return
        /// </summary>
        /// <returns></returns>
        static Func<int, int, string> Test22()
        {
            Func<int, int, string> a = (i1, i2) => $"{i1} & {i2}";
            return a;
        }

        /// <summary>
        /// 如果只有一个参数，参数的 () 可以省略
        /// </summary>
        /// <returns></returns>
        static Func<int, bool> Test23()
        {
            Func<int, bool> a = i => i > 5;
            return a;
        }
    }
}