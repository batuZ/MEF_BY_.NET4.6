using AM_Interface;
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

namespace AM_Main
{
    public partial class Form1 : Form
    {
        [ImportMany]
        Lazy<IToolsPlugin, IToolsInfo>[] lazy_tools;
        //切换语言事件
        Action<string> SetLangvage = (s) => { /* 保证事件不为空 */};
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //查找本地组件，塞入集合，没有的话也可以不找
            AssemblyCatalog assList = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            AggregateCatalog all_Files = new AggregateCatalog(assList);

            //查找外部组件，塞入集合
            DirectoryCatalog ortherCatalog = new DirectoryCatalog(@"Plugins\", "*.dll");
            all_Files.Catalogs.Add(ortherCatalog);

            //组装到主程序
            CompositionContainer CC = new CompositionContainer(all_Files);
            CC.ComposeParts(this);//---->组装成功！


            //整理组件,生成按钮并指定动作
            if (lazy_tools != null)
            {
                List<ToolStripItem> tsi = new List<ToolStripItem>();
                foreach (var item in lazy_tools)
                {
                    //创建按钮
                    ToolStripMenuItem plugMenuItem = new ToolStripMenuItem()
                    {
                        Name = $"{item.Metadata.toolName}ToolStripMenuItem",
                        Size = new Size(152, 22),
                        Text = item.Metadata.toolName
                    };
                    //绑点击事件,并传参
                    plugMenuItem.Click += new EventHandler((object obj, EventArgs ee) => { item.Value.Run(new object[] { "", 2, false }); });
                    //绑切换语言事件
                    SetLangvage += item.Value.ChangeLangvage;
                    //绑销毁事件
                    this.FormClosing += new FormClosingEventHandler((object o, FormClosingEventArgs ee) => item.Value.Dispose());

                    //按钮集合
                    tsi.Add(plugMenuItem);
                }
                //把按钮加入菜单栏
                autoLoadBarToolStripMenuItem.DropDownItems.AddRange(tsi.ToArray());
            }
        }

    }
}
