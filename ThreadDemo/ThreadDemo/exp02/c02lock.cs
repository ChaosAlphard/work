using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo.exp02 {
  class c02lock {
    private static void print(string str) {
      Console.WriteLine(str);
    }
    /**
     * 排它锁用于确保同一时间只允许一个线程执行指定的代码段。
     * 主要的两个排它锁构造是lock和Mutex（互斥体）。
     * 其中lock更快，使用也更方便。
     * 而Mutex的优势是它可以跨进程的使用。
     * 在这一节里，我们从介绍lock构造开始，
     * 然后介绍Mutex和信号量（semaphore）（用于非排它场景）。
     * 稍后在第 4 部分会介绍读写锁（reader / writer lock）。
     * Framework 4.0 加入了SpinLock结构体，可以用于高并发场景。
     */
    static int _val1 = 1, _val2 = 1;
    static int _lval1 = 1, _lval2 = 1;
    static int _l2val1 = 1, _l2val2 = 1;
    static readonly object _locker = new object();

    public void tran01() {
      print("锁");
      /**
       * 可能会产生除数为零错误，
       * 因为可能在一个线程刚好执行完
       * if的判断语句但还没执行
       * Console.WriteLine语句时，
       * _val2就被另一个线程设置为零。
       */
      print("=================");
      Thread t = new Thread(Go);
      t.Start();
      Go();
      // 使用lock解决这个问题
      print("=================");
      Thread lt = new Thread(LockerGo);
      lt.Start();
      LockerGo();

      /**
       * 同一时间只有一个线程可以锁定同步对象（这里指_locker），
       * 并且其它竞争锁的线程会被阻塞，直到锁被释放。
       * 如果有多个线程在竞争锁，
       * 它们会在一个“就绪队列（ready queue）”中排队，
       * 并且遵循先到先得的规则
       * （需要说明的是，Windows 系统和 CLR 的差别
       * 可能导致这个队列在有时会不遵循这个规则）。
       * 因为一个线程的访问不能与另一个线程相重叠，
       * 排它锁有时也被这样描述：
       * 它强制对锁保护的内容进行顺序（serialized）访问。
       * 在这个例子中，我们保护的是Go方法的内部逻辑，还有_val1与_val2字段。
       * 
       * 在竞争锁时被阻塞的线程，它的线程状态是WaitSleepJoin。
       * 在中断与中止中，我们会描述如何通过其它线程强制释放被阻塞的线程，
       * 这是一种可以用于结束线程的重型技术
       * （这里指它们应该被作为在没有其它更为优雅的办法时的最后手段）。
       */
    }

    static void Go() {
      if (_val2 != 0) {
        Console.WriteLine(_val1 / _val2);
      }
      _val2 = 0;
    }

    static void LockerGo() {
      lock (_locker) {
        if (_lval2 != 0) {
          Console.WriteLine(_lval1 / _lval2);
        }
        _lval2 = 0;
      }
    }

    public void tran02() {
      print("Monitor.Enter / Monitor.Exit");
      /**
       * lock其实是一个语法糖，它使用了 try/finally 
       * 来调用Monitor.Enter 与 Monitor.Exit
       */
      Monitor.Enter(_locker);
      try {
        if (_l2val2 != 0) {
          Console.WriteLine(_l2val1 / _l2val2);
        }
        _l2val2 = 0;
      } finally {
        Monitor.Exit(_locker);
      }
      /* 如果在同一个对象上没有先调用
       * Monitor.Enter就调用Monitor.Exit
       * 会抛出一个异常。 */

      /**
       * Note: lockTaken重载 / TryEnterPermalink
       * https://blog.gkarch.com/threading/part2.html#locktaken-overloads
       */
    }

    private static List<string> _list = new List<string>();
    public void tran03() {
      print("选择同步对象");
      /**
       * 对所有参与同步的线程可见的任何对象
       * 都可以被当作同步对象使用，
       * 但有一个硬性规定：同步对象必须为引用类型。
       * 同步对象一般是私有的（因为这有助于封装锁逻辑），
       * 并且一般是一个实例或静态字段。
       * 同步对象也可以就是其要保护的对象，
       * 如下面例子中的_list字段：
       */

      Thread t = new Thread(listlocker);
      t.Start();

      listlocker();
      t.Join();
      _list.ForEach(i => {
        Console.WriteLine(i);
      });

      /**
       * 一个只被用来加锁的字段（例如前面例子中的_locker）
       * 可以精确控制锁的作用域与粒度。对象自己（this），
       * 甚至是其类型都可以被当作同步对象来使用：
       * lock (this) { ... }
       * // 或者：
       * lock (typeof (Widget)) { ... }// 保护对静态资源的访问
       * 这种方式的缺点在于并没有对锁逻辑进行封装，
       * 从而很难避免死锁与过多的阻塞。
       * 同时类型上的锁也可能会跨越应用程序域
       * （application domain）边界（在同一进程内）。
       * 你也可以在被 lambda 表达式或匿名方法所捕获的局部变量上加锁。
       *
       * PS: 锁在任何情况下都不会限制对同步对象本身的访问。
       * 换句话说，x.ToString()不会因为其它线程调用lock(x)而阻塞，
       * 两个线程都要调用lock(x)才能使阻塞发生。
       */
    }

    static void listlocker() {
      lock (_list) {
        _list.Add("Item 1");
        _list.Add("Item 2");
      }
    }

    /**
      * Note: 何时加锁
      * https://blog.gkarch.com/threading/part2.html#when-to-lock
      * 
      * Note: 锁与原子性
      * https://blog.gkarch.com/threading/part2.html#locking-and-atomicity
      * 如果一组变量总是在相同的锁内进行读写，
      * 就可以称为原子的（atomically）读写。
      * 假定字段x与y总是在对locker对象的lock内进行读取与赋值：
      * lock (locker) { if (x != 0) y /= x; }
      * 可以说x和y是被原子的访问的，
      * 因为上面的代码块无法被其它的线程分割或抢占。
      * 
      * Note: 嵌套锁
      * https://blog.gkarch.com/threading/part2.html#nested-locking
      * 线程可以用嵌套（重入）的方式重对相同的对象进行加锁：
      * lock (locker) {
      *   lock (locker) {
      *     lock (locker) {
      *       // ...
      *     } } }
      * 或者：
      * Monitor.Enter(locker);Monitor.Enter(locker);Monitor.Enter(locker);
      * // ...
      * Monitor.Exit (locker);Monitor.Exit (locker);Monitor.Exit (locker);
      * 
      * 在这样的场景中，只有当最外层的lock语句退出
      * 或是执行了匹配数目的Monitor.Exit语句时，
      * 对象才会被解锁。
      */

    /**
     * Note: 死锁
     * https://blog.gkarch.com/threading/part2.html#deadlocks
     * 当两个线程等待的资源都被对方占用时，
     * 它们都无法执行，这就产生了死锁。
     * 如何避免? 流行的建议：“以一致的顺序对对象加锁以避免死锁”，
     * 尽管它对于我们最初的例子有帮助，
     * 但是很难应用到刚才所描述的场景。
     * 更好的策略是：
     * 如果发现在锁区域中的对其它类的方法调用最终会引用回当前对象，
     * 就应该小心，同时考虑是否真的需要对其它类的方法调用加锁
     * （往往是需要的，但是有时也会有其它选择）。
     * 更多的依靠声明方式（declarative）与
     * 数据并行（data parallelism）、
     * 不可变类型（immutable types）与
     * 非阻塞同步构造(nonblocking synchronization constructs)，
     * 可以减少对锁的需要。
     */

    /**
     * Note: 性能
     * https://blog.gkarch.com/threading/part2.html#performance
     */

    /**
     * Note: 互斥体
     * https://blog.gkarch.com/threading/part2.html#mutex
     * 互斥体类似于 C# 的lock，
     * 不同在于它是可以跨越多个进程工作。
     * 换句话说，Mutex可以是机器范围（computer-wide）的，
     * 也可以是程序范围（application-wide）的。
     * 
     * PS: 没有竞争的情况下，
     * 获取并释放Mutex需要几微秒的时间，
     * 大约比lock慢 50 倍。
     */
    public void tran04() {
      print("Mutex(互斥锁)");
      /**
       * 使用Mutex类时，可以调用WaitOne方法来加锁，
       * 调用ReleaseMutex方法来解锁。
       * 关闭或销毁Mutex会自动释放锁。
       * 与lock语句一样，Mutex只能被获得该锁的线程释放。
       * 
       * PS: 跨进程Mutex的一种常见的应用就是确保只运行一个程序实例。
       */
      using (var mutex = new Mutex(false, "unique MutexID")) {
        // 可能 其它程序实例正在关闭
        // 可以等待几秒来让其它实例完成关闭
        if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false)) {
          Console.WriteLine("Another App is running. Exit");
          return;
        }
        Run();
      }
      /**
       * 如果在终端服务（Terminal Services）下运行，
       * 机器范围的Mutex默认仅对于
       * 运行在相同终端服务器会话的应用程序可见。
       * 要使其对所有终端服务器会话可见，
       * 需要在其名字前加上`Global\`。
       */
    }
    public static void Run() {
      Console.WriteLine("Running...");
    }

    /**
     * Note: 信号量
     * https://blog.gkarch.com/threading/part2.html#semaphore
     * 信号量：相当于具有一定容量的线程队列。
     * 一旦装满，就不允许其他线程再进入，这些线程将在外面排队。
     * 当有一个线程离开时，排在最前的线程便可以进入。
     * 这种构造最少需要两个参数：队列当前的空位数以及队列的总容量。
     * 
     * 容量为 1 的信号量与Mutex和lock类似，
     * 所不同的是信号量没有“所有者”，
     * 它是线程无关（thread-agnostic）的。
     * 任何线程都可以在调用Semaphore上的Release方法，
     * 而对于Mutex和lock，只有获得锁的线程才可以释放。
     * 
     * PS: SemaphoreSlim是 Framework 4.0 加入的轻量级的信号量，
     * 功能与Semaphore相似，
     * 不同之处是它对于并行编程的低延迟需求做了优化。
     * 在传统的多线程方式中也有用，
     * 因为它支持在等待时指定取消标记 （cancellation token）。
     * 但它不能跨进程使用。
     * 在Semaphore上调用WaitOne或Release会产生大概 1 微秒的开销，
     * 而SemaphoreSlim产生的开销约是其四分之一。
     */
    private static SemaphoreSlim _sem = new SemaphoreSlim(3);
    public void tran05() {
      print("Semaphore(信号量)");
      for(int i=0; i<5; i++) {
        new Thread(Enter).Start(i);
      }
    }

    public static void Enter(object o) {
      print($"{o} wants to enter");
      // 同时只有3个线程, 超过则排队
      _sem.Wait();
      print($"{o} is enter");
      // 模拟线程操作
      Thread.Sleep((int)o * 1000);
      print($"{o} is leave");
      // 释放
      _sem.Release();
    }
    /**
     * 如果Sleep语句被替换为密集的磁盘 I/O 操作，
     * 由于Semaphore限制了过多的并发硬盘活动，
     * 就可能改善整体性能。
     * PS: 类似于Mutex，命名的Semaphore也可以跨进程使用。
     */

    /**
     * Note: 线程安全
     * https://blog.gkarch.com/threading/part2.html#thread-safety
     */

  } // class
}
