using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Program
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            myCom cmd = new myCom();
            Console.WriteLine("ProgramStart...");
            while (!exit)
            {
                cmd.InitPlugin();
                if (cmd.plugins.Count() == 0)
                    exit = true;
                Console.WriteLine($"Found Plugins : {  cmd.plugins.Count()}");
                List<MEF_Interface.WriteTool> tempList = new List<MEF_Interface.WriteTool>();
                foreach (MEF_Interface.WriteTool item in cmd.plugins)
                {
                    Console.Write($"{tempList.Count + 1},{item.toolName}  ");
                    tempList.Add(item);
                }
                Console.WriteLine(";");
                Console.Write("inPut Number to Run([e]exit):");
                ConsoleKeyInfo p = Console.ReadKey(true);
               
                try
                {
                    if (p.Key == ConsoleKey.E)
                        exit = true;
                    else
                    {
                        string str = Chr(p.KeyChar);
                        int i = Convert.ToInt32(str) - 1;
                        Console.WriteLine(tempList[i].toolName);
                        tempList[i].Run();
                    }
                }
                catch
                {

                }
            }
        }
        static string Chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                ASCIIEncoding asciiEncoding = new ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }
    }
    class myCom
    {
        #region MEF - MainPart

        //不过滤，直接导入所有模块
        [ImportMany]

        //带导入标识符，与[Export(typeof(IPlugin))]对应,起到过滤作用
        //[Import(typeof(IPlugin))]

        //组装好的功能模块入口集合，遍历成员，通过自定义的标识调用对应功能
        //IPlugin 公开接口
        public IEnumerable<MEF_Interface.WriteTool> plugins;

        //设置功能模块存放目录
        private string pluginPath = AppDomain.CurrentDomain.BaseDirectory + "plugin\\";

        //这个函数可在程序运行时调用，重新发现，实现热插拔
        public void InitPlugin()
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
