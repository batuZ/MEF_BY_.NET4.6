using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using MefDemo;

namespace MefDemo
{
    [Export(typeof(IPlugin))]
    public class MyPlugin2:IPlugin
    {
        public string Text
        {
            get { return "插件2"; }
        }

        public void Do()
        {
            System.Windows.Forms.MessageBox.Show(Text);
        }
    }
}
