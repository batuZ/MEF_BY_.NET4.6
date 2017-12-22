using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 多组件延迟加载_2
{
    /*
     
	1. 多组件时，为了区分功能不同的组件，需要获取组件信息，但同时也触发了组件的初始化，失去了延迟加载的意义
	2. 为组件附加信息，加载时只获取附加信息而不初始化组件，来实现区分功能同时延迟加载。

	方法2：
		声明一个LoggerDataAttribute，这个Attribute必须被MetadataAttribute标记。
		然后，在Export的对象前加上该LoggerDataAttribute，这样MEF导入的时候就会根据该LoggerDataAttribute创建元数据了。

		这里的LoggerDataAttribute本身并不需要实现ILoggerData接口，它是一个DuckType的约定，只需要实现元数据的属性即可。
		我这里实现该接口主要是为了让编译器保障元数据属性都有被准确实现。

        */

    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.cPart();
            p.y.ToList().ForEach(t => Console.WriteLine($"{t.Metadata.ModuleName} -> {t.Metadata.Index}"));  //  <--------------------- 获取附加信息
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
        int Index { get; }
    }

    [MetadataAttribute]
    //[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    class Infos : Attribute, Person_info    //  <------------------------------- 用一个新类继承信息接口，封装一下
    {
        public int Index
        {
            get; private set;
        }

        public string ModuleName
        {
            get; private set;
        }
        public Infos(string a)
        {
            ModuleName = a;
        }
        public Infos(string a, int f)
        {
            Index = f;
            ModuleName = a;
        }
    }
    #endregion



    #region <------------------------------- 组件
    [Infos("Chinese", 4)] // <------------------------------- 用新新类塞入信息
    [Export(typeof(Person))]
    public class Chinese : Person
    {
        public string SayHello(string name)
        {
            return "你好：" + name;
        }
    }

    [Infos("American", 5)]// <------------------------------- 导出时附加组件信息
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
