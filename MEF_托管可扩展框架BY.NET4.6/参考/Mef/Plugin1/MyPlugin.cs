using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using MefDemo;
using System.Windows.Forms;

namespace MefDemo
{
    [Export(typeof(IPlugin))]
    public class MyPlugin : IPlugin
    {
        public string Text
        {
            get { return "插件1"; }
        }

        public void Do()
        {
            MessageBox.Show(Text);
        }
    }
}
