# MEF BY .NET4.6 


###	[官方介绍](https://msdn.microsoft.com/zh-cn/library/dd460648(v=vs.110).aspx)

	Managed Extensibility Framework 即 MEF 是用于创建轻量、可扩展应用程序的库。
	它让应用程序开发人员得以发现和使用扩展且无需配置。 
	它还让扩展开发人员得以轻松地封装代码并避免脆弱的紧密依赖性。
	MEF 让扩展不仅可在应用程序内重复使用，还可以跨程序重复使用。

	MEF 是 .NET Framework 4 的组成部分，适用于所有使用 .NET Framework 的地方。 
	你可以在客户端应用程序（不论其是否使用 Windows 窗体或其他技术）或使用 ASP.NET 的服务端应用程序里使用 MEF。

	.NET Framework 的早期版本引入了 Managed Add-in Framework (MAF)，旨在让应用程序隔离和托管扩展。 
	MAF 与 MEF 相比，MAF 的重点级别较高，它关注扩展隔离和程序集加载及卸载，而 MEF 则关注可发现性、可扩展性和可移植性。 
	这两个框架可以平稳地相互操作，且一个单独的应用程序可以利用这两者。

###	[基本概念](https://www.cnblogs.com/content/archive/2013/05/31/3111156.html)

	MEF：Managed Extensibility Framework，.NET 4.0中带来的一个基于托管的扩展程序开发框架, 其实MEF是为您的解决方案打破紧耦合的依赖。

	Contract：契约，即一种约定，具体在代码中表现为接口和抽象类。

	Import：导入，是部件向要通过可用导出满足的容器提出的要求, 可修饰字段、属性或构造函数参数。

	Export：导出，是部件向容器中的其他部件提供的一个值, 可修饰类、字段、属性或方法。

	注：

	1.为了使导入与导出匹配，导入和导出必须具有相同的协定。 协定由一个字符串（称为"协定名称"）和已导出或导入对象的类型（称为“协定类型”）组成。只有在协定名称和协定类型均匹配时，才会认为导出能够满足特定导入。

	2.协定参数中的其中任意一个或两者可能为隐式也可能为显式。

	3.通常应对公共类或成员声明导出和导入。其他声明也受支持，但如果导出或导入私有成员、受保护成员或内部成员，将会损坏部件的隔离模型，因此建议不要这样做。

	Part：部件，即实现契约的类。

	Catalog：目录（理解意义），存放部件的地方，当需要某个部件时，会在目录中寻找。

	Container：容器，存放目录并进行部件管理，如导出、导入等。

	Compose：组装，通过容器在目录中寻找到实现了相应契约的部件，进行部件的组装。

### 更多应用示例

##### 案例1 对象传递

```C#
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
```

##### 案例2 属性（字段）传递

```C#
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
```

##### 案例3 方法（事件）传递
```C#
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
```
#####  案例4 多对像传递

```C#
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
```

#####  案例5 [延迟加载](https://www.cnblogs.com/TianFang/p/4069978.html)
```C#
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
```