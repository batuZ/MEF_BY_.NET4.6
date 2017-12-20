using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MefDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitPlugin();
            InitializeComponent();
            item = new ToolStripMenuItem("插件");
            ms.Items.Add(item);
        }
        ToolStripMenuItem item;
        private void Form1_Load(object sender, EventArgs e)
        {
            item.DropDownItems.Clear();
            foreach (IPlugin plugin in plugins)
            {
                ToolStripMenuItem subItem = new ToolStripMenuItem(plugin.Text);
                subItem.Click += (s, arg) => { plugin.Do(); };
                item.DropDownItems.Add(subItem);
            }
        }
        private void aaaaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitPlugin();
            Form1_Load(null, null);
        }



        #region MEF - MainPart

        //不过滤，直接导入所有模块
        [ImportMany]

        //带导入标识符，与[Export(typeof(IPlugin))]对应,起到过滤作用
        //[Import(typeof(IPlugin))]

        //组装好的功能模块入口集合，遍历成员，通过自定义的标识调用对应功能
        public IEnumerable<IPlugin> plugins;

        //设置功能模块存放目录
        string pluginPath = AppDomain.CurrentDomain.BaseDirectory + "plugin\\";

        //这个函数可在程序运行时调用，重新发现，实现热插拔
        private void InitPlugin()
        {
            DirectoryCatalog ModuleDir = new DirectoryCatalog(pluginPath);

            //创建功能模块目录，并把目录中所有模块塞进目录（不包括子目录）
            AggregateCatalog ModuleList = new AggregateCatalog();
            ModuleList.Catalogs.Add(ModuleDir);

            //用功能模块目录创建一个容器，模块就是容器中的零件，准备组合进宿主程序
            CompositionContainer _container = new CompositionContainer(ModuleList);

            //调用容器的组合零件函数 ComposeParts() 把各个部件组合到一起
            try
            {
                //这里传入宿主程序实例，MEF会自动发现并组装
                _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
        #endregion

    }
}
