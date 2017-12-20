using MEF_Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF_ClassLib_DLL
{
    [Export(typeof(IPlugin))]
    public class ClassLib : IPlugin
    {
        public ClassLib() { Console.WriteLine("dll类库插件,已初始化！"); }
        public string text
        {
            get
            {
                return "dll类库插件";
            }
        }

        public void init()
        {
            Console.WriteLine("dll类库插件Start!");
        }
    }
}
