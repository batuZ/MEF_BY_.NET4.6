using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF_Master
{
    public interface IPlugin
    {
        string text { get; }
        void init();
    }
    public interface ILazyPlugin
    {
        string text { get; }
        int id { get; }
        void init();
    }
    public interface IEnumerable<T1, T2>
    {
    }
}
/*
  /// <summary>
  /// 案例1 对象传递 ----------------------------------------------------------
  /// </summary>
  class Program1
  {
      //导入对象使用
      [Import("chinese_hello")]
      public Person oPerson { set; get; }

      static void Main(string[] args)
      {
          var oProgram = new Program1();
          oProgram.MyComposePart();
          var strRes = oProgram.oPerson.SayHello("李磊");
          Console.WriteLine(strRes);
          Console.Read();
      }

      //宿主MEF并组合部件
      void MyComposePart()
      {
          var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
          var container = new CompositionContainer(catalog);
          //将部件（part）和宿主程序添加到组合容器
          container.ComposeParts(this);
      }
  }

  public interface Person
  {
      string SayHello(string name);
  }

  //声明对象可以导出
  [Export("chinese_hello", typeof(Person))]
  public class Chinese : Person
  {
      public string SayHello(string name)
      {
          return "你好：" + name;
      }
  }

  [Export("american_hello", typeof(Person))]
  public class American : Person
  {
      public string SayHello(string name)
      {
          return "Hello:" + name;
      }
  }
*/

 /*案例2 属性（字段）传递
   class Program2
  {
      [Import("TestProperty")]
      public string ConsoleTest { get; set; }

      static void Main(string[] args)
      {
          var oProgram = new Program2();
          oProgram.MyComposePart();

          Console.WriteLine(oProgram.ConsoleTest);

          Console.Read();
      }

      void MyComposePart()
      {
          var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
          var container = new CompositionContainer(catalog);
          //将部件（part）和宿主程序添加到组合容器
          container.ComposeParts(this);
      }
  }

  public class TestPropertyImport
  {
      [Export("TestProperty")]
      public string TestMmport { get { return "测试属性可以导入导出"; } }
  }
  */


/*案例3 方法（事件）传递 ---------------------------------------------------

  class Program3
 {
     [Import("chinese_hello")]
     public Person oPerson { set; get; }

     [Import("TestProperty")]
     public string ConsoleTest { get; set; }

     [Import("helloname")]
     public Action<string> TestFuncImport { set; get; }

     static void Main(string[] args)
     {
         var oProgram = new Program3();
         oProgram.MyComposePart();
         oProgram.TestFuncImport("Jim");


         //Console.WriteLine(oProgram.ConsoleTest);
         //var strRes = oProgram.oPerson.SayHello("李磊");
         //Console.WriteLine(strRes);
         Console.Read();
     }

     void MyComposePart()
     {
         var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
         var container = new CompositionContainer(catalog);
         //将部件（part）和宿主程序添加到组合容器
         container.ComposeParts(this);
     }
 }
 public class TestPropertyImport
 {
     [Export("TestProperty")]
     public string TestMmport { get { return "测试属性可以导入导出"; } }

     [Export("helloname", typeof(Action<string>))]
     public void GetHelloName(string name)
     {
         Console.WriteLine("Hello：" + name);
     }
 }
 */


/* 案例4 多对像传递
 * 使用ImportMany的时候对应的Export不能有chinese_hello这类string参数，否则lstPerson的Count()为0.*

   class Program4
  {
      [ImportMany]
      public IEnumerable<Person> lstPerson { set; get; }

      static void Main(string[] args)
      {
          var oProgram = new Program4();
          oProgram.MyComposePart();

          Console.WriteLine(oProgram.lstPerson.Count());

          Console.Read();
      }

      void MyComposePart()
      {
          var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
          var container = new CompositionContainer(catalog);
          //将部件（part）和宿主程序添加到组合容器
          container.ComposeParts(this);
      }
  }

public interface Person
  {
      string SayHello(string name);
  }

  [Export(typeof(Person))]
  public class Chinese : Person
  {
      public string SayHello(string name)
      {
          return "你好：" + name ;
      }
  }

  [Export(typeof(Person))]
  public class American : Person
  {
      public string SayHello(string name)
      {
          return "Hello:" + name ;
      }
  }
*/


/* 案例5 延迟加载
class Program2
{
    [Import("chinese_hello")]
    public Person oPerson { set; get; }

    [Import("american_hello")]
    public Lazy<Person> oPerson2 { set; get; }

    static void Main(string[] args)
    {
        var oProgram = new Program2();
        oProgram.MyComposePart();

        var strRes = oProgram.oPerson.SayHello("李磊");
        var strRes2 = oProgram.oPerson2.Value.SayHello("Lilei");
        Console.WriteLine(strRes);

        Console.Read();
    }

    void MyComposePart()
    {
        var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
        var container = new CompositionContainer(catalog);
        //将部件（part）和宿主程序添加到组合容器
        container.ComposeParts(this);
    }
}

public interface Person
{
    string SayHello(string name);
}

[Export("chinese_hello", typeof(Person))]
public class Chinese : Person
{
    public string SayHello(string name)
    {
        return "你好：" + name;
    }
}

[Export("american_hello", typeof(Person))]
public class American : Person
{
    public string SayHello(string name)
    {
        return "Hello:" + name;
    }
}
*/
