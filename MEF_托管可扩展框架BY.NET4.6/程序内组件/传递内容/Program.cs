using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 传递内容
{
    /*
        *  1.   传递类时，接收到的是这个类的实例，在组件被调用时初始化，无法在宿主中初始化另一个实例
        *  2.   也就是说，组件的类被当作单例类或静态类来用了
        *  3.   传递属性和函数是不需要接口的，只要用类型和名称来约定
        *  4.   也就是说，这样的传递方式并不安全
        */

    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.cPart();
           
            Console.WriteLine(p.Pi.ToString());

            p.sayhi("李磊");

            Console.ReadKey(true);
        }

        #region <---------------------- 宿主获取组件

        //接收属性（字段）
        [Import("TestPI")]
        public double Pi;

        //接收函数（事件）
        [Import(typeof(Action<string>))]
        public Action<string> sayhi;


        void cPart()
        {
            // 程序（项目）内查找组件
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        #endregion
    }


    #region <------------------------------- 组件

    public class American
    {
        //传递属性（字段）,不需要接口定义
        [Export("TestPI")]
        public double PI = 3.14;

        //传递函数（事件）,不需要接口定义
        [Export(typeof(Action<string>))]
        public void SayHi(string name)
        {
            Console.WriteLine("Hi:" + name);
        }
    }
    #endregion
}
