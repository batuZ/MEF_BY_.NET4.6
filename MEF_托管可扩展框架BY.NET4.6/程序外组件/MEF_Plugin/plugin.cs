using MEF_InterfaceP;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF_Plugin
{
    [Export(typeof(PPP))]
    public class plugin : PPP
    {
        public void sayHi()
        {
            Console.WriteLine("Hi plugin");
        }
    }
}
