using System;
using System.Threading;

namespace ThreadDemo {
  class JoinAndSleep {
    public void init() {
      Thread t = new Thread(Go);
      t.Start();
      // 等待`T`线程执行完成
      t.Join();
      // 使线程等待(阻塞)一定的时间
      // PS: 阻塞(blocked)状态的线程, 不会消耗 CPU 资源。
      Thread.Sleep(1000);
      /** Notes
       * Thread.Sleep(0)会立即释放当前的时间片
       * 将 CPU 资源出让给其它线程
       * Framework 4.0 新的Thread.Yield()方法与其相同
       * 除了它只会出让给运行在相同处理器核心上的其它线程。
       * 
       * Sleep(0)和Yield在调整代码性能时偶尔有用
       * 它也是一个很好的诊断工具
       * 可以用于找出线程安全（thread safety）的问题
       * 如果在你代码的任意位置插入Thread.Yield()会影响到程序
       * 基本可以确定存在 bug。
       */
      Console.WriteLine("Thread T is Ended!");
    }

    public void Go() {
      for (int i = 0; i < 100; i++) {
        Console.Write("T");
      }
    }

  }
}
