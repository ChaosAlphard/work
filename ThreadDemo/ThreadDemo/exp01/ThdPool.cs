using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo {
  class ThdPool {
    /**
     * 当启动一个线程时，会有几百毫秒的时间花费在准备一些额外的资源上
     * 例如一个新的私有局部变量栈这样的事情。
     * 每个线程会占用（默认情况下）1MB 内存。
     * 线程池（thread pool）可以通过共享与回收线程来减轻这些开销，
     * 允许多线程应用在很小的粒度上而没有性能损失。
     * 在多核心处理器以分治（divide-and-conquer）
     * 的风格执行计算密集代码时将会十分有用。
     * 线程池会限制其同时运行的工作线程总数。
     * 太多的活动线程会加重操作系统的管理负担，
     * 也会降低 CPU 缓存的效果。
     * 一旦达到数量限制，任务就会进行排队，
     * 等待一个任务完成后才会启动另一个。
     * 这使得程序任意并发成为可能，例如 web 服务器。
     * PS: 以下构造会间接使用线程池：
     * - WCF、Remoting、ASP.NET 和 ASMX 网络服务应用
     * - System.Timers.Timer 和 System.Threading.Timer
     * - .NET Framework 中名字以 Async 结尾的方法，
     *     例如WebClient上的方法（使用基于事件的异步模式，EAP），
     *     和大部分BeginXXX方法（异步编程模型模式，APM）
     * - PLINQ
     */

    /**
     * 任务并行库（Task Parallel Library，TPL）
     * 与 PLINQ 足够强大并面向高层，
     * 即使使用线程池并不重要，
     * 也应该使用它们来辅助多线程。
     * PS: 在使用线程池线程时有几点需要小心：
     * - 无法设置线程池线程的Name属性，这会令调试更为困难
     *     （当然，调试时也可以在 Visual Studio 的线程窗口中给线程附加备注）。
     * - 线程池线程永远是后台线程（一般不是问题）。
     * - 阻塞线程池线程可能会在程序早期带来额外的延迟，
     *     除非调用了ThreadPool.SetMinThreads（见优化线程池）。
     * 
     * 可以改变线程池线程的优先级，当它用完后返回线程池时会被恢复到默认状态。
     * PS: 可以通过Thread.CurrentThread.IsThreadPoolThread
     *     属性来查询当前是否运行在线程池线程上。
     */
    public void init() {
      Console.WriteLine("线程池");
      Console.WriteLine("通过 TPL 使用线程池");
      /**
       * 可以很容易的使用任务并行库（Task Parallel Library，TPL）中的
       * Task类来使用线程池。Task类在 Framework 4.0 时被加入：
       * 如果你熟悉旧式的构造，
       * 可以将非泛型的Task类看作
       * ThreadPool.QueueUserWorkItem的替代，
       * 而泛型的Task<TResult>看作异步委托的替代。
       * 比起旧式的构造，新式的构造会更快速，更方便，并且更灵活。
       * PS: 要使用非泛型的Task类，
       * 调用Task.Factory.StartNew，
       * 并传递目标方法的委托：
       */
      Task t = Task.Factory.StartNew(go);
      /**
       * Task.Factory.StartNew返回一个Task对象，
       * 可以用来监视任务，例如通过调用Wait方法来等待其结束。
       * 当调用Task的Wait方法时，
       * 所有未处理的异常会在宿主线程被重新抛出。
       * 如果不调用Wait而是丢弃不管，
       * 未处理的异常会像普通的线程那样结束程序。
       * PS: 在 .NET4.5 中，
       * 为了支持基于async / await的异步模式，
       * Task中这种“未观察”的异常默认会被忽略，
       * 而不会导致程序结束。
       */
    }

    static void go() {
      Console.WriteLine("Hello from the thread pool!");
    }

    public void init01() {
      /**
       * 泛型的Task<TResult>类是非泛型Task的子类。
       * 它可以使你在其完成执行后得到一个返回值。
       */
      Task<string> task = Task.Factory.StartNew(
        () => Download("http://www.gkarch.com")
      );
      Console.WriteLine("线程Task<TResult>");
      string result = task.Result;
      Console.WriteLine(result.Length);
    }

    static string Download(string url) {
      using (var wc = new System.Net.WebClient()) {
        return wc.DownloadString(url);
      }
    }

    /**
     * 如果是使用 .NET Framework 4.0 以前的版本，
     * 则不能使用任务并行库。
     * 必须通过一种旧的构造使用线程池：
     * ThreadPool.QueueUserWorkItem与异步委托。
     * 这两者之间的不同在于异步委托可以让你从线程中返回数据，
     * 同时异步委托还可以将异常封送回调用方。
     */
    public void init02() {
      Console.WriteLine("不通过 TPL 使用线程池");
      Console.WriteLine("- QueueUserWorkItemPermalink");
      // 要使用QueueUserWorkItem
      // 仅需要使用希望在线程池线程上运行的委托来调用该方法：
      ThreadPool.QueueUserWorkItem(Go);
      ThreadPool.QueueUserWorkItem(Go, 123);
    }

    static void Go(object data) {
      /**目标方法Go
       * 必须接受单一一个object参数
       * （来满足WaitCallback委托）。
       * 这提供了一种向方法传递数据的便捷方式，
       * 就像ParameterizedThreadStart一样。
       * 与Task不同，QueueUserWorkItem
       * 并不会返回一个对象来帮助我们在后续管理其执行。
       * 并且，你必须在目标代码中显式处理异常，
       * 未处理的异常会令程序结束。
       */
      Console.WriteLine("Hello from the thread pool! " + data);
    }

    /**
     * ThreadPool.QueueUserWorkItem
     * 并没有提供在线程执行结束之后从线程中返回值的简单机制。
     * 异步委托调用（asynchronous delegate invocations ）
     * 解决了这一问题，可以允许双向传递任意数量的参数。
     * 而且，异步委托上的未处理异常可以方便的原线程上重新抛出
     * （更确切的说，在调用EndInvoke的线程上），
     * 所以它们不需要显式处理。
     * 
     * PS: 不要混淆异步委托和异步方法
     * （asynchronous methods
     * 以 Begin 或 End 开始的方法
     * 比如File.BeginRead/File.EndRead）
     * 异步方法表面上看使用了相似的方式
     * 但是其实是为了解决更困难的问题。
     */
    public void init03() {
      /**
       * 下面是如何通过异步委托启动一个工作线程：
       * - 创建目标方法的委托（通常是一个Func类型的委托）。
       * - 在该委托上调用BeginInvoke，保存其IAsyncResult类型的返回值。
       *   BeginInvokde会立即返回。当线程池线程正在工作时，你可以执行其它的动作。
       * - 当需要结果时，在委托上调用EndInvoke，传递所保存的IAsyncResult对象。
       */
      Console.WriteLine("- 异步委托");

      Func<string, int> method = Work;
      IAsyncResult cookie = method.BeginInvoke("test", null, null);
      //
      // 这里可以并行执行其它任务
      //
      int result = method.EndInvoke(cookie);
      Console.WriteLine("String length is: " + result);
      /**
       * EndInvoke会做三件事：
       * - 如果异步委托还没有结束，它会等待异步委托完成执行。
       * - 它会接收返回值（也包括ref和out方式的参数）。
       * - 它会向调用线程抛出未处理的异常。
       * 
       * PS: 如果使用异步委托调用的方法没有返回值
       *   技术上你仍然需要调用EndInvoke
       * 即: 无论使用何种方法，
       *   都要调用 EndInvoke 来完成异步调用。
       */
    }

    static int Work(string s) { return s.Length; }

    /**
     * 当调用BeginInvoke时也可以指定一个回调委托。
     * 这是一个在完成时会被自动调用的、
     * 接受IAsyncResult对象的方法。
     * 这样可以在后面的代码中“忘记”异步委托，
     * 但是需要在回调方法里做点其它工作
     */
    static void Star() {
      Func<string, int> method = Work;
      method.BeginInvoke("test", Done, method);
      // ...
      //
    }
    static void Done(IAsyncResult cookie) {
      var target = (Func<string, int>)cookie.AsyncState;
      int result = target.EndInvoke(cookie);
      Console.WriteLine("String length is: " + result);
    }
    /**
     * BeginInvoke的最后一个参数是一个用户状态对象，
     * 用于设置IAsyncResult的AsyncState属性。
     * 它可以是需要的任何东西，
     * 在这个例子中，我们用它向回调方法传递method委托，
     * 这样才能够在它上面调用EndInvoke。
     */

    public void init04() {
      Console.WriteLine("优化线程池");
      /**
       * 线程池初始时其池内只有一个线程。
       * 随着任务的分配，
       * 线程池管理器就会向池内“注入”新线程来满足工作负荷的需要，
       * 直到最大数量的限制。
       * 在足够的非活动时间之后，
       * 线程池管理器在认为 `回收` 一些线程
       * 能够带来更好的吞吐量时进行线程回收。
       * 
       * 可以通过调用ThreadPool.SetMaxThreads
       * 方法来设置线程池可以创建的线程上限；默认如下：
       * - Framework 4.0，64位环境下：32768
       * - Framework 4.0，32位环境下：1023
       * - Framework 3.5：每个核心 250
       * - Framework 2.0：每个核心 25
       * （这些数字可能根据硬件和操作系统不同而有差异。）
       * 数量这么多是因为要确定阻塞
       * （等待一些条件，比如远程计算机的相应）
       * 的线程的条件是否被满足。
       * 
       * 也可以通过ThreadPool.SetMinThreads
       * 设置线程数量下限。下限的作用比较奇妙：
       * 它是一种高级的优化技术，
       * 用来指示线程池管理器在达到下限之前不要延迟线程的分配。
       * 当存在阻塞线程时，提高下限可以改善程序并发性。
       * 
       * PS: 默认下限数量是 CPU 核心数，
       * 也就是能充分利用 CPU 的最小数值。
       * 在服务器环境下（比如 IIS 中的 ASP.NET），
       * 下限数量一般要高的多，差不多 50 或者更高。
       */
    }

  }// class
}
