using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo.exp02 {
  class c01sync {

    private static void print(string str) {
      Console.WriteLine(str);
    }

    public c01sync() {
      Console.WriteLine("e02c01 同步");
    }

    /**
     * 同步（synchronization）：
     * 为期望的结果协调线程的行为。
     * 当多个线程访问同一个数据时，
     * 同步尤其重要，
     * 同步构造可以分为以下四类：
     * - 简单的阻塞方法
     *     这些方法会使当前线程等待另一个线程结束或是自己等待一段时间。
     *     Sleep、Join与Task.Wait都是简单的阻塞方法。
     * - 锁构造
     *     锁构造能够限制每次可以执行某些动作或是执行某段代码的线程数量。
     *     排它锁构造是最常见的，它每次只允许一个线程执行，
     *     从而可以使得参与竞争的线程在访问公共数据时不会彼此干扰。
     *     标准的排它锁构造是lock（Monitor.Enter/Monitor.Exit）、
     *     Mutex与 SpinLock。非排它锁构造是Semaphore、SemaphoreSlim以及读写锁。
     * - 信号构造
     *     信号构造可以使一个线程暂停，直到接收到另一个线程的通知，
     *     避免了低效的轮询 。有两种经常使用的信号设施：
     *     事件等待句柄（event wait handle ）和Monitor类的Wait / Pluse方法。
     *     Framework 4.0 加入了CountdownEvent与Barrier类。
     * - 非阻塞同步构造
     *     非阻塞同步构造通过调用处理器指令来保护对公共字段的访问。
     *     CLR 与 C# 提供了下列非阻塞构造：
     *     Thread.MemoryBarrier 、Thread.VolatileRead、
     *     Thread.VolatileWrite、volatile关键字
     *     以及Interlocked类。
     */

    private static void go() {
      print("go!");
    }

    public void tran01() {
      print("阻塞");
      /**
       * 当线程的执行由于某些原因被暂停，
       * 比如调用Sleep等待一段时间，
       * 或者通过Join或EndInvoke方法等待其它线程结束时，
       * 则认为此线程被阻塞（blocked）。
       * 被阻塞的线程会立即出让（yields）其处理器时间片，
       * 之后不再消耗处理器时间，直到阻塞条件被满足。
       * 可以通过线程的ThreadState属性来检查一个线程是否被阻塞：
       */
      var t = new Thread(go);
      bool blocked = (t.ThreadState & ThreadState.WaitSleepJoin) != 0;
      Console.WriteLine($"blocked = {blocked}");

      /**
       * 上面例子中线程状态可能在进行状态判断和依据状态进行操作之间发生改变，
       * 因此这段代码仅可用于调试诊断的场景。
       * 
       * 当一个线程被阻塞或是解除阻塞时，
       * 操作系统会进行上下文切换（context switch），
       * 这会带来几微秒的额外时间开销。
       * 
       * 阻塞会在以下 4 种情况下解除（电源按钮可不能算╮(╯▽╰)╭）：
       * - 阻塞条件被满足
       * - 操作超时（如果指定了超时时间）
       * - 通过Thread.Interrupt中断
       * - 通过Thread.Abort中止
       * 通过Suspend方法（已过时，不应该再使用）暂停线程的执行不被认为是阻塞。
       */
    }

    public void tran02() {
      print("阻塞和自旋");
      /**
       * 有时线程必须暂停，直到特定条件被满足。
       * 信号构造和锁构造可以通过在条件被满足前阻塞线程来实现。
       * 但是还有一种更为简单的方法：
       * 线程可以通过自旋（spinning）来等待条件被满足。例如：
       * while (!proceed);
       * 或者：
       * while (DateTime.Now < nextStartTime)
       * >
       * 一般来说，这会非常浪费处理器时间：
       * 因为对 CLR 和操作系统来说，这个线程正在执行重要的计算，
       * 就给它分配了相应的资源。
       * 有时会组合使用阻塞与自旋：
       * while (!proceed) Thread.Sleep (10);
       *
       * 尽管并不优雅，但是这比仅使用自旋更高效（一般来说）。
       * 然而这样也可能会出现问题，
       * 这是由proceed标识上的并发问题引起的。
       * 正确的使用和锁构造和信号构造可以避免这个问题。
       * 自旋在等待的条件很快（大致几微秒）就能被满足的情况下更高效，
       * 因为它避免了上下文切换带来的额外开销
       */
    }

    public void tarn03() {
      print("线程状态");
      /**
       * 可以通过线程的ThreadState属性来查询线程状态，
       * 它会返回一个ThreadState类型的按位方式组合的枚举值，
       * 其中包含了三“层”信息。
       */

      /**
       * 下面的代码可以提取线程状态中最有用的 4 个值: 
       * Unstarted、Running、WaitSleepJoin和Stopped：
       *
       * public static ThreadState SimpleThreadState (ThreadState ts) {
       *   return ts & (ThreadState.Unstarted |
       *          ThreadState.WaitSleepJoin |
       *          ThreadState.Stopped);
       * }
       * 
       * PS: ThreadState属性在进行调试诊断时有用，
       * 但不适合用来进行同步，
       * 因为线程状态可能在判断状态和依据状态进行操作之间发生改变。
       */
    }

  }
}
