## 程序内组件

	接口、组件都在宿主程序（项目）内定义

	通过C# Assembly.GetExecutingAssembly()获取组件集合，与程序外获取组件的方式有所不同。

	ps: 没理解这种形式如何实现低耦合,弱依赖。


#### 字符串匹配组件
	
	通过约定名称，从类型相同的组件集合中，找到唯一匹配的模块，需要注意的要点：

	1.  [Export("chinese_hello", typeof(Person))] 字符串 + 类型 
    2.  [Import("chinese_hello")] 这时，typeof(Person)类型且名称为"chinese_hello"的组件必须唯一
    3.  程序（项目）内查找组件：Assembly.GetExecutingAssembly()
    4.  类型不同时，可以同名，不会出错

#### 字符串匹配多个同名同类型组件

	通过约定名称\类型，找到匹配的模块集合

	1.  [ImportMany("chinese_hello")] 
	2.  public Person[] y;

#### 类型匹配组件

	找到类型相同的唯一匹配的模块，需要注意的要点：

	1.  [Export(typeof(Person))] 导出时只声明类型 ,不可以带名称
	2.  [Import(typeof(Person))] 这时，typeof(Person)类型的组件必须唯一

#### 类型匹配组件集合

	找到类型相同的模块集合，需要注意的要点：
	1.  [ImportMany(typeof(Person))] 
	2.  public Person[] y

#### 传递内容

	传递属性、方法
	1.   传递类时，接收到的是这个类的实例，在组件被调用时初始化，无法在宿主中初始化另一个实例
	2.   也就是说，组件的类被当作单例类或静态类来用了
	3.   传递属性和函数是不需要接口的，只要用类型和名称来约定
	4.   也就是说，这样的传递方式并不安全