using MEF_Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF_ControlNote1
{

    // forDeBug
    class Program
    {
        static void Main(string[] args)
        {
            ControlPlugin1 ss = new ControlPlugin1();
            ss.init();
        }
    }

    //forExportPlugin
    [Export(typeof(IPlugin))]
    public class ControlPlugin1 : IPlugin
    {
        public ControlPlugin1()
        {
            Console.WriteLine("控制台插件1,已初始化！");
        }
        public string text
        {
            get
            {
                return "控制台插件1";
            }
        }

        public void init()
        {
            Console.WriteLine("workStart!");
        }
    }
}
