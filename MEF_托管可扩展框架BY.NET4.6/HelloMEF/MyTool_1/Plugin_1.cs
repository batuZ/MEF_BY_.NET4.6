using MEF_Interface;
using System.ComponentModel.Composition;
using System;
using System.Windows.Forms;

namespace MyTool_1
{
    //模块1
    [Export(typeof(WriteTool))]
    public class Plugin_1 : WriteTool
    {
        public string toolName
        {
            get
            {
                return "Plugin_1";
            }
        }

        public void Run()
        {
            MessageBox.Show("Plugin_1 is Running...");
        }
    }


    //模块2
    [Export(typeof(WriteTool))]
    public class Plugin_2 : WriteTool
    {
        public string toolName
        {
            get
            {
                return "Plugin_2";
            }
        }

        public void Run()
        {
            MessageBox.Show("Plugin_2 is Running...");
        }
    }
}
