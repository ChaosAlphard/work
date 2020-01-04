using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.exp03 {
  class c01backgroundworker {
    private static void print(string s) {
      Console.WriteLine(s);
    }

    /**
     * Note: 基于事件的异步模式
     * https://blog.gkarch.com/threading/part3.html#the-event-based-asynchronous-pattern
     * 基于事件的异步模式（event-based asynchronous pattern，EAP）提供了一种简单的方式，让类可以提供多线程的能力，而不需要使用者显式启动和管理线程。它也提供如下的功能：
     * - 协作取消模型（cooperative cancellation model）
     * - 工作线程完成时安全更新 WPF 或 Windows Forms 控件的能力
     * - 转发异常至完成事件
     * 
     * PS: EAP 仅仅是一个模式，所以这些功能需要自行实现。
     */
    static BackgroundWorker _worker = new BackgroundWorker();
    public void tran01() {
      print("backgroundworker");

      _worker.DoWork += doWork;
      _worker.RunWorkerAsync("Message");
    }

    private static void doWork(object sender, DoWorkEventArgs e) {
      print(e.Argument.ToString());
    }
  }
}
