using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
      var ct = new CreateThread();
      ct.init();
      ct.init01();
      ct.init02();
      Console.ReadLine();
    }
  }
}
