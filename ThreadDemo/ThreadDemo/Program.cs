using System;
using ThreadDemo.exp02;
using ThreadDemo.exp03;

namespace ThreadDemo {
  class Program {
    static void Main(string[] args) {
      /* 基础 */
      //var c01 = new Class01();
      //c01.init();

      /* Join 和 Sleep */
      //var jas = new JoinAndSleep();
      //jas.init();

      /* 创建线程与传递参数 */
      //var ct = new CreateThread();
      //ct.init();
      //ct.init01();
      //ct.init02();

      /* 线程命名 与 前台/后台线程 */
      //var ct = new ThreadNaming();
      //ct.init();
      //ct.init01(0);

      /* 线程池 */
      //var ct = new ThdPool();
      //ct.init();
      //ct.init01();

      /* 锁 */
      //var ct = new c02lock();
      //ct.tran01();
      //ct.tran03();
      //ct.tran04();
      //ct.tran05();

      /* backgroundworker */
      var e = new c01backgroundworker();
      e.tran01();

      Console.WriteLine("Done. press enter key to exit");
      Console.ReadLine();
    }
  }
}
