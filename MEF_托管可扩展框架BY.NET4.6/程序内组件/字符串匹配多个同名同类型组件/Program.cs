﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 字符串匹配多个同名同类型组件
{
    /*
        *  通过约定名称\类型，找到匹配的模块集合
        *  需要注意的要点：
        *  1.  [ImportMany("chinese_hello")] 
        *  2.  public IEnumerable<Person> y;
        *  3.  遍历 y
        */

    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.cPart();
            foreach (var item in p.y)
            {
                Console.WriteLine(item.SayHello("李磊"));
            }
          
            Console.ReadKey(true);
        }

        #region <---------------------- 宿主获取组件
        // 通过约定名称获取组件
        [ImportMany("chinese_hello")]
        public Person[] y;
        void cPart()
        {
            // 程序（项目）内查找组件
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        #endregion
    }

    #region <------------------------------- 接口
    public interface Person
    {
        string SayHello(string name);
    }
    #endregion

    #region <------------------------------- 组件
    //声明对象可以导出
    [Export("chinese_hello", typeof(Person))]
    public class Chinese : Person
    {
        public string SayHello(string name)
        {
            return "你好：" + name;
        }
    }

    [Export("chinese_hello", typeof(Person))]
    public class American : Person
    {
        public string SayHello(string name)
        {
            return "Hello:" + name;
        }
    }
    #endregion
}
