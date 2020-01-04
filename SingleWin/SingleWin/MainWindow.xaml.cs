using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SingleWin {
  /// <summary>
  /// MainWindow.xaml 的交互逻辑
  /// </summary>
  public partial class MainWindow : Window {
    Mutex mutex = new Mutex(false, "Single WPF");

    public MainWindow() {
      if(hasAnotherInstance(mutex)) {
        MessageBox.Show("程序已经启动了");
        this.Close();
        Application.Current.Shutdown();
      } else {
        InitializeComponent();
      }
    }

    private void initial(object sender, EventArgs e) {
      Console.WriteLine("初始化完成");
    }

    private bool hasAnotherInstance(Mutex mutex) {
      return !mutex.WaitOne(
        TimeSpan.FromSeconds(1), false
      );
    }
  } // class
}
