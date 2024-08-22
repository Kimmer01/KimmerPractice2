using System.Collections;
using System.Collections.Generic;

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

            List<int> ints = new List<int>() { 10, 5, 80, 8 };
            IEnumerable<int> wherePracticeResult = WherePractice(ints, i => i > 10);
            Console.WriteLine(string.Join(",", wherePracticeResult));
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

        /// <summary>
        /// 1. LINQ 中提供了很多 集合(IEnumerable) 的扩展方法(大部分都在 System.Linq 命名空间中)，配合 lambda 能简化数据处理
        /// <p>For example:
        /// .Any(), .Count(), .Single(), .SingleOrDefault(), .First(), .FirstOrDefault()
        /// 返回值 IOrderedEnumerable: .OrderBy().ThenBy(), .OrderByDescending().ThenByDescending()
        /// .Skip()
        /// .Take(3) 取最多3条，如果只有2条，则取2条
        /// 聚合函数：.Max(), .Min(), Average(), .Sum(), .Count()
        /// 返回值 IGrouping<TKey, TSource>: .GroupBy()
        /// 投影操作: list.Select(l => new Class(l)); 把集合中的每一项转换成另一种类型
        /// 匿名类型: var newList = list.Select(l => new {Name = "AAA", Age=10});
        /// 以上是 ‘LINQ方法语法’，还有一种‘查询语法’的写法，和写 SQL 类似。两种写法，编译器编译的结果是一样的
        ///     from l in list
        ///     where l.Age>10
        ///     orderby l.Salary
        ///     select new {Age = l.Age, Gender = l.Gender?"男":"女"};
        /// </p>
        /// 2. yield return '流水线'式处理，效率更高
        /// 3. C# 中的 var 依然是强类型的，编译时会根据赋值判断类型。编译器的“类型推断”
        /// </summary>
        static IEnumerable<T> WherePractice<T>(IEnumerable<T> list, Func<T, bool> func)
        {
            foreach (T item in list)
            {
                if (func(item))
                {
                    yield return item;
                }
            }

        }
    }
}