using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using MefDemo;

namespace MefDemo
{
    [Export(typeof(IPlugin))]
    public class MyPlugin3:IPlugin
    {
        public string Text
        {
            get { return "插件31"; }
        }

        public void Do()
        {
            new Form1().ShowDialog();
        }
    }
}
