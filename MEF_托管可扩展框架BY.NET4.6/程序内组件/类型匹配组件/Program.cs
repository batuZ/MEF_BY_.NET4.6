using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 类型匹配组件
{
    /*
        *  找到类型相同的唯一匹配的模块
        *  需要注意的要点：
        *  1.  [Export(typeof(Person))] 导出时只声明类型 ,不可以带名称
        *  2.  [Import(typeof(Person))] 这时，typeof(Person)类型的组件必须唯一
        */

    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.cPart();
            Console.WriteLine(p.y.SayHello("李磊"));
            Console.ReadKey(true);
        }

        #region <---------------------- 宿主获取组件
        // 通过约定名称获取组件
        [Import(typeof(Person))]
        public Person y;
        void cPart()
        {
            // 程序（项目）内查找组件
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        #endregion
    }

    #region <------------------------------- 接口
    public interface Person
    {
        string SayHello(string name);
    }
    #endregion

    #region <------------------------------- 组件

    //声明对象可以导出
    [Export("chinese_hello", typeof(Person))]
    public class Chinese : Person
    {
        public string SayHello(string name)
        {
            return "你好：" + name;
        }
    }

    [Export(typeof(Person))]
    public class American : Person
    {
        public string SayHello(string name)
        {
            return "Hello:" + name;
        }
    }
    #endregion
}
