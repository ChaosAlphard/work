using System;
using System.Threading;

namespace ThreadDemo {
  class ThreadNaming {
    /**
     * 每一个线程都有一个 Name 属性，
     * 我们可以设置它以便于调试。
     * 这在 Visual Studio 中非常有用，
     * 因为线程的名字会显示在线程窗口（Threads Window）
     * 与调试位置（Debug Location）工具栏上。
     * 线程的名字只能设置一次，以后尝试修改会抛出异常。
     * PS: 静态的Thread.CurrentThread属性会返回当前执行的线程。
     */

     public void init() {
      Console.WriteLine("线程命名");
      Thread.CurrentThread.Name = "main";
      Thread t = new Thread(Go);
      t.Name = "Go";
      t.Start();
      Go();
    }

    static void Go() {
      Console.WriteLine("Hello from " + Thread.CurrentThread.Name);
    }

    /**
     * 默认情况下，显式创建的线程都是前台线程（foreground threads）。
     * 只要有一个前台线程在运行，
     * 程序就可以保持存活，而后台线程（background threads）
     * 并不能保持程序存活。
     * 当一个程序中所有前台线程停止运行时，
     * 仍在运行的所有后台线程会被强制终止。
     * 
     * 可以通过线程的IsBackground属性来查询或修改线程的前后台状态。
     * 
     * PS: 线程的前台/后台状态与它的优先级和执行时间的分配无关。
     */
    public void init01(int lv) {
      Console.WriteLine("前台/后台线程");

      Thread worker = new Thread(() => Console.ReadLine());
      if (lv == 0) { worker.IsBackground = true; }
      worker.Start();
      /**
       * 如果lv != 0，工作线程会默认为前台，
       * 并在ReadLine时等待用户输入回车。
       * 此时主线程退出，但是程序仍然在运行，
       * 因为有一个前台线程依然存活。
       * 
       * 相反，如果lv == 0，工作线程设置为后台状态，
       * 当主线程结束时，
       * 程序几乎立即退出（终止ReadLine需要时间）。
       * 当进程以这种方式结束时，
       * 后台线程执行栈中 所有 finally块 就会被 避开。
       * 
       * 如果程序依赖finally（或是using）块来执行清理工作
       * 例如释放资源或是删除临时文件，
       * 就可能会产生问题。
       * 为了避免这种问题，
       * 在退出程序时可以显式的等待这些后台线程结束。
       * 
       * 有两种方法可以实现：
       * 如果是自己创建的线程，在线程上调用Join方法。
       * 如果是使用线程池线程，使用事件等待句柄。
       * 在任一种情况下，都应指定一个超时时间，
       * 从而可以放弃由于某种原因而无法正常结束的线程。
       */
    }

    /**
     * 线程的Priority属性决定了相对于操作系统中的其它活动线程
     * 它可以获得多少执行时间。线程优先级的取值如下：
     * enum ThreadPriority { 
     *   Lowest, BelowNormal,
     *   Normal, AboveNormal,
     *   Highest
     * }
     * 只有当多个线程同时活动时，线程优先级才有意义。
     * 
     * 提升线程优先可能会导致其它线程的资源饥饿(resource starvation)
     * (指没有分配到足够的CPU时间来运行) 等问题。
     * 
     * PS: 提升线程的优先级是无法使其能够处理实时任务的
     * 因为它还受到程序进程优先级的影响。
     * 要进行实时任务，
     * 必须同时使用System.Diagnostics中的Process类来提升进程的优先级
     * using (Process p = Process.GetCurrentProcess())
     *   { p.PriorityClass = ProcessPriorityClass.High; }
     */

    /**
     * ProcessPriorityClass.High
     * 实际上就是一个略低于最高优先级Realtime的级别。
     * 将一个进程的优先级设置为Realtime是通知操作系统，
     * 我们绝不希望该进程将 CPU 时间出让给其它进程。
     * 如果你的程序误入一个无限循环，
     * 会发现甚至是操作系统也被锁住了，
     * 正是由于这一原因，High 通常是实时程序的最好选择。
     * 
     * PS: 如果实时程序拥有用户界面，
     * 提升进程的优先级会导致大量的 CPU 时间被用于屏幕更新，
     * 这会降低整台机器的速度(特别是当 UI 很复杂时)。
     * 降低主线程的优先级，
     * 并提升进程的优先级可以保证需要进行实时任务的工作线程不会被屏幕重绘所抢占。
     * 但是这依然没有解决其它程序的CPU时间饥饿的问题，
     * 因为操作系统依然为这个进程分配了大量 CPU 资源。
     * 理想的解决方案是分离 UI 线程和实时工作线程，
     * 使用两个进程分别运行。
     * 这样就可以分别设置各自的进程优先级，
     * 彼此之间通过 Remoting 或是内存映射文件进行通信。
     * 内存映射文件十分适用于这一任务
     */

    /**
     * 即使是提升了进程优先级
     * 托管环境在处理强实时需求时仍然有限制。
     * 除了自动垃圾回收带来的延迟，
     * 操作系统也不能够完全满足实时任务的需要，
     * 即便是非托管的程序也是如此。
     * 最好的解决办法还是使用独立的硬件或者专门的实时平台。
     */

    /**
     * 当线程开始运行后，其创建代码所在的
     * try / catch / finally块与该线程不再有任何关系。
     */
    static void init02() {
      try {
        new Thread(nullErr);
      } catch {
        Console.WriteLine("Exception");
      }
      /**
       * 这个例子中的try / catch语句是无效的
       * 而新创建的线程将会遇到
       * 一个未处理的NullReferenceException
       * 当你考虑到每一个线程具有独立的执行路径时
       * 这种行为就可以理解了
       */
    }

    static void nullErr() { throw null; }

    static void fixedErr() {
      try {
        throw null;
      } catch {
        Console.WriteLine("Exception");
      }
      /**
       * 在生产环境的程序中，
       * 所有线程的入口方法处都应该有一个异常处理器，
       * 就如同在主线程所做的那样（一般可能是在执行栈上靠近入口的地方）。
       * 未处理的异常会使得整个程序停止运行，弹出一个难看的对话框。
       */
    }

    /**
     * WPF 和 Windows Forms 应用中的
     * `全局`异常处理事件
     * （Application.DispatcherUnhandledException
     * 和Application.ThreadException）
     * 只会在主UI线程有未处理的异常时触发。
     * 对于工作线程上的异常仍然需要手动处理。
     * AppDomain.CurrentDomain.UnhandledException
     * 会对所有未处理的异常触发，
     * 但是它不提供阻止程序退出的办法。
     */

  }
}
