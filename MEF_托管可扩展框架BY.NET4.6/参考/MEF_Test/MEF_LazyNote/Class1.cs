using MEF_Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF_LazyNote
{
    [Export(typeof(ILazyPlugin))]
    public class ClassLaz : ILazyPlugin
    {
        public ClassLaz()
        {
            Console.WriteLine("类库实时加载部件,初始化！");
        }
        public int id
        {
            get
            {
                return 106;
            }
        }

        public string text
        {
            get
            {
                return "类库实时加载部件";
            }
        }

        public void init()
        {
            Console.WriteLine("类库实时加载部件:Start!");
        }
    }
}
