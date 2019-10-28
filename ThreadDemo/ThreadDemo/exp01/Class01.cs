using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo {
  class Class01 {
    private bool done01 = false;
    private static bool done02 = false;

    private void isDone() {
      if (!done01) {
        done01 = true;
        Console.WriteLine("Done!");
      }
    }

    private void isStaticDone() {
      if (!done02) {
        Console.WriteLine("Done!");
        done02 = true;
      }
    }

    // 排它锁 (exclusive lock)
    private static readonly object locker = new object();
    private static bool done03 = false;

    private void lockerDone() {
      /**
       * 当两个线程同时争夺一个锁的时候（locker）
       * 一个线程等待，或者说阻塞, 直到锁变为可用。
       * 这样就确保了在同一时刻只有一个线程能进入
       * 临界区（critical section，不允许并发执行的代码）
       * 所以 “ Done “ 只被打印了一次。
       * 像这种用来避免在多线程下的
       * 不确定性的方式被称为线程安全（thread-safe）。
       * PS: 线程被阻塞时，不会消耗 CPU 资源。
       */
      lock (locker) {
        if (!done03) {
          Console.WriteLine("Done!");
          done03 = true;
        }
      }
    }

    public void init() {
      // 创建一个公共实例
      Class01 c01 = new Class01();

      /**
       * 由于两个线程是调用了同一个的 Class01 实例上的 isDone()
       * 它们共享了`done`字段
       * 因此输出结果是一次`Done`
       * 而不是两次。
       */
      new Thread(c01.isDone).Start();
      c01.isDone();

      Console.WriteLine("==========static==========");

      /**
       * 静态字段提供了另一种在线程间共享数据的方式
       * PS: 可能会打印两次 Done
       */
      new Thread(c01.isStaticDone).Start();
      c01.isStaticDone();

      Console.WriteLine("==========locker==========");

      new Thread(c01.lockerDone).Start();
      c01.isStaticDone();
    }

  }
}
