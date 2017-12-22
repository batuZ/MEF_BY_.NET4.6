using MEF_InterfaceP;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 内外同时加载
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.InitPlugin();
            p.y.ToList().ForEach(f => { f.sayHi(); });
            Console.ReadKey(true);
        }

        [ImportMany]
        public PPP[] y;

        public void InitPlugin()
        {
            //创建空的零件集合
            AggregateCatalog catalog = new AggregateCatalog();
            //获取外部零件
            DirectoryCatalog Dll_List = new DirectoryCatalog(@"D:\MyCode\MEF_BY_.NET4.6\MEF_托管可扩展框架BY.NET4.6\程序外组件\MEF_Plugin\bin\Debug");
            //获取程序内,标记的可导出的零件
            AssemblyCatalog Ass_List = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            //分别塞入集合
            catalog.Catalogs.Add(Dll_List);
            catalog.Catalogs.Add(Ass_List);
            
            //创建合成容器
            CompositionContainer container = new CompositionContainer(catalog);
            //过滤合成到当前程序实例
            container.ComposeParts(this);
        }
    }



    [Export(typeof(PPP))]
    public class American : PPP
    {
        public void sayHi()
        {
            Console.WriteLine("Hi American");
        }
    }
}
