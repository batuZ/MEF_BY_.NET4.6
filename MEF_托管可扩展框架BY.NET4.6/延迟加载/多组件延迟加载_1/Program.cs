using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 多组件延迟加载_1
{
    /*
     
	1. 多组件时，为了区分功能不同的组件，需要获取组件信息，但同时也触发了组件的初始化，失去了延迟加载的意义
	2. 为组件附加信息，加载时只获取附加信息而不初始化组件，来实现区分功能同时延迟加载。

	方法1：
		新创建一个接口，导出组件同时附加新接口的键值对，键对应接口中的属性名，值对应组件的识别内容

		加载组件时，变为 public Lazy<Person, PersonData>[] y 
		PersonData为附加信息，y[0].Metadata.Name 就是组件的识别符
		这样 Person 并没有被调用，也就不会被加载

        */

    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.cPart();
            p.y.ToList().ForEach(t => Console.WriteLine(t.Metadata.ModuleName));  //  <--------------------- 获取附加信息
            Console.ReadKey(true);
        }

        #region <---------------------- 宿主获取组件
        [ImportMany(typeof(Person))]
        public Lazy<Person, Person_info>[] y;    //  <------------------------------- 加载时附加信息

        void cPart()
        {
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
    public interface Person_info     //  <------------------------------- 增加一个附加信息接口
    {
        string ModuleName { get; }
    }
    #endregion



    #region <------------------------------- 组件
    [ExportMetadata("ModuleName", "Chinese")] // <------------------------------- 导出时附加组件信息 ModuleName 必须对应 Person_info 中的属性名
    [Export(typeof(Person))]
    public class Chinese : Person
    {
        public string SayHello(string name)
        {
            return "你好：" + name;
        }
    }

    [ExportMetadata("ModuleName", "American")] // <------------------------------- 导出时附加组件信息
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
