using System;

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
      var ct = new ThdPool();
      ct.init();



      Console.WriteLine("Done. press enter key to exit");
      Console.ReadLine();
    }
  }
}
