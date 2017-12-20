using MEF_Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEF_FormNote
{
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
    [Export(typeof(IPlugin))]
    public class formPlugIn : IPlugin
    {
        public formPlugIn()
        {
            Console.WriteLine("窗体插件,已初始化！");
        }
        public string text
        {
            get
            {
                return "窗体插件";
            }
        }

        public void init()
        {
            new Form1().Show();
        }
    }
}
