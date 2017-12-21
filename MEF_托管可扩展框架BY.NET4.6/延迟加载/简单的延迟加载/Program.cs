using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 简单的延迟加载
{
    /*
    *  需要注意的要点：
    *  1.  接收组件的类型 T 变为 Lazy<T>
    *  2.  用Value调用组件
    */
    
    class Program
    {
        [Import("pu")]
        public Lazy<string> y;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.cPart();
            Console.WriteLine(p.y.Value);
            Console.ReadKey(true);
        }

        void cPart()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }

    class Module
    {
        [Export("pu")]
        public string name = "hahaha";
    }
}
