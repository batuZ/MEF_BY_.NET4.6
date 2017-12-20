using MEF_Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEF_FormNote2
{
    //forDeBug

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    //forExportPlugin

    [Export(typeof(IPlugin))]
    public class formPlugin2 : IPlugin
    {
        public formPlugin2()
        {
            Console.WriteLine("窗体插件2,已初始化！");
        }
        public string text
        {
            get
            {
                return "窗体插件2";
            }
        }

        public void init()
        {
            Form1 fs = new Form1();
            fs.Show();
        }
    }
}
