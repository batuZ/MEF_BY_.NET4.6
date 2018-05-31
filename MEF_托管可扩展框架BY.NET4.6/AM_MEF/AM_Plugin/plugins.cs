using AM_Interface;
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace AM_Plugin
{
    [SetToolInfo("Tool_A")]
    [Export(typeof(IToolsPlugin))]
    public class Tool_A : IToolsPlugin
    {
        public void ChangeLangvage(string TagLangvage)
        {
            Console.WriteLine($"变更语言为{TagLangvage}");
        }

        public void Dispose()
        {
            Console.WriteLine("工具已关闭，清理垃圾！");
        }

        public void Run(object[] args)
        {
            MessageBox.Show("Tool_A Start!");
        }
    }

    [SetToolInfo("Tool_B")]
    [Export(typeof(IToolsPlugin))]
    public class Tool_B : IToolsPlugin
    {
        public void ChangeLangvage(string TagLangvage)
        {
            Console.WriteLine($"变更语言为{TagLangvage}");
        }

        public void Dispose()
        {
            Console.WriteLine("工具已关闭，清理垃圾！");
        }

        public void Run(object[] args)
        {
            MessageBox.Show("Tool_B Start!");
        }
    }
}
