using MEF_InterfaceP;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Program v = new Program();
            v.InitPlugin();
            Console.WriteLine($"ModuleCount : {v.p.Length}");
            v.p.ToList().ForEach(h => { h.sayHi(); });
            Console.ReadKey(true);
        }

        
        [ImportMany]
        public PPP[] p;

        public void InitPlugin()
        {
            DirectoryCatalog Dir = new DirectoryCatalog(
                @"C:\code\MAX\MEF_BY_.NET4.6\MEF_托管可扩展框架BY.NET4.6\程序外组件\MEF_Plugin\bin\Debug");
            AggregateCatalog List = new AggregateCatalog();
            List.Catalogs.Add(Dir);
            CompositionContainer ner = new CompositionContainer(List);
            try
            {
                ner.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
