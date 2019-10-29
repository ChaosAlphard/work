using System;
using System.Threading;

namespace ThreadDemo {
  class CreateThread {
    // 使用Thread类的构造方法来创建线程
    // 通过传递 委托 来指明线程从哪里开始运行
    //public delegate void ThreadStart();

    public void init() {
      //完整语法
      var t = new Thread(new ThreadStart(Go));
      t.Start();

      //线程也可以使用更简洁的语法创建
      //使用方法组（method group）
      //让 C# 编译器推断ThreadStart委托类型：
      var t01 = new Thread(Go);
      t01.Start();

      // 也可使用Lambad
      var t02 = new Thread(()=>Go());
      t02.Start();

      Go();
    }

    static void Go() {
      Console.WriteLine("Hello!");
    }

    // 向线程传递数据
    public void init01() {
      // 最简单的方式是使用 lambda 表达式调用目标方法，在表达式内指定参数
      Thread t = new Thread(() => print("Hello from t!"));
      t.Start();

      // 也可使用匿名方法
      new Thread(delegate () {
        print("Hello from delegate thread");
      }).Start();

      //或者向Start 传递参数(msgObject)
      //但是方法里通常需要类型转换
      new Thread(printWithMsg).Start("Hello from Start");
    }

    private static void print(string msg) {
      Console.WriteLine(msg);
    }

    private static void printWithMsg(object msgObject) {
      Console.WriteLine(msgObject as string);
    }

    // Lambda 与被捕获变量
    public void init02() {
      /**
       * 变量i在整个循环中指向相同的内存地址
       * 所以，每一个线程在调用Console.Write时
       * 都在使用这个值在运行时会被改变的变量！
       */
      for(int i=0; i<10; i++) {
        new Thread(() => { print(i.ToString()); }).Start();
      }
      // 同以下
      string tmp = "1";
      var tt1 = new Thread(() => print(tmp));
      tmp = "2";
      var tt2 = new Thread(() => print(tmp));
      tt1.Start();
      tt2.Start();
    }

  }
}
