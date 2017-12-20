using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEF_Master
{
    public partial class Form1 : Form
    {
        [ImportMany]
        IEnumerable<Lazy<IPlugin>> Plugins;
        IEnumerable<Lazy<ILazyPlugin>> LazyPlugins;
        public Form1()
        {
            InitializeComponent();
            Init();

        }
        void Init()
        {
            //总目录
            AggregateCatalog catalog = new AggregateCatalog();

            ///查找本地【动态】部件
            //AssemblyCatalog locals = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            //AssemblyCatalog locals = new AssemblyCatalog(typeof(Program).Assembly);
            //catalog.Catalogs.Add(locals);

            ///查找目录下所有可加载部件
            string plugPath = AppDomain.CurrentDomain.BaseDirectory + "plugin\\";
            List<string> plugDir = Directory.GetDirectories(plugPath).ToList<string>();

            for (int i = 0; i < plugDir.Count; i++)
            {
                DirectoryCatalog pluginEXEParts = new DirectoryCatalog(plugDir[i], "*.*");
                catalog.Catalogs.Add(pluginEXEParts);
            }
            //用部件创建组合容器
            CompositionContainer container = new CompositionContainer(catalog);
            try
            {
                //这里只需要传入当前应用程序实例就可以了，其它部分会自动发现并组装
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int count = Plugins.Count();
            if (count > 0)
            {
                ToolStripMenuItem item = new ToolStripMenuItem("插件");
                menuStrip1.Items.Add(item);
                foreach (Lazy<IPlugin> plugin in Plugins)
                {
                    ToolStripMenuItem subItem = new ToolStripMenuItem(plugin.GetType().ToString());
                    subItem.Click += createIt(plugin);
                    item.DropDownItems.Add(subItem);
                }
                //foreach (IPlugin plugin in LazyPlugins)
                //{
                //    ToolStripMenuItem subItem = new ToolStripMenuItem(plugin.text);
                //    subItem.Click += (s, arg) => { plugin.init(); };
                //    item.DropDownItems.Add(subItem);
                //}
            }
        }
        EventHandler createIt(Lazy<IPlugin> plugin)
        {
            return (s, arg) => { plugin.Value.init(); };
        }

        private void tool01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("begin");
            try
            {
                File.Open("", FileMode.Open);
                Console.WriteLine("inTry");
            }
            catch
            {
                Console.WriteLine("inCatch");
            }
            finally
            {
                Console.WriteLine("inFinally");
            }
            Console.WriteLine("outSide");
        }
    }


}
