using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace Udp {
  /// <summary>
  /// MainWindow.xaml 的交互逻辑
  /// </summary>
  public partial class MainWindow : Window {
    private delegate void changeReceive(string msg);

    private Client client = new Client();

    private bool __isOpen = false;
    private Thread recTh = null;

    private bool isOpen {
      get {
        return __isOpen;
      }
      set {
        __isOpen = value;
        aSend.IsReadOnly = !value;
        xLocalPort.IsReadOnly = xSendBtn.IsEnabled = value;
        if(value) {
          xBindLocalPortBtn.Content = "Rebind";
        } else {
          xBindLocalPortBtn.Content = "Bind";
        }
      }
    }

    public MainWindow() {
      InitializeComponent();
    }

    private void toggleBind(object sender, RoutedEventArgs e) {
      if (!isOpen) {
        try {
          if (client.checkLocalClient()) {
            client.closeClient();
            client.discardClient();
          }
          openPort();
          listen();
          isOpen = true;
          xTip.Text = "[√]Bind succ!";
        } catch (FormatException) {
          MessageBox.Show("Port格式错误, 请检查输入");
          isOpen = false;
          xTip.Text = "[×]Bind fail!";
        } catch(SocketException) {
          MessageBox.Show("该端口已被占用");
        } catch (ThreadAbortException te) {
          MessageBox.Show($"{te}");
        } catch (Exception) {
          isOpen = false;
          xTip.Text = "[×]Bind fail!";
        }
      } else {
        try {
          unlisten();
          closePort();
          isOpen = false;
        } catch(NullReferenceException) {
          isOpen = false;
        } catch(ThreadAbortException te){
          MessageBox.Show(te.ToString());
        } catch(Exception err) {
          MessageBox.Show(err.ToString());
        }
      }
    }

    private void openPort() {
      int n = Convert.ToInt32(xLocalPort.Text.Trim());
      client = new Client(n);
    }

    private void listen() {
      recTh = new Thread(receiveDataThread) {
        IsBackground = true
      };
      recTh.Start();
    }

    private void receiveDataThread() {
      var iep = new IPEndPoint(IPAddress.Any, 0);
      string recStr = "";

      while (true) {
        byte[] recByte = client.getClient().Receive(ref iep);
        recStr += Encoding.UTF8.GetString(recByte);
        // aReceive.Text = recStr;
        aReceive.Dispatcher.Invoke(
          new changeReceive(changeRecAction),
          recStr
        );
      }

    }

    private void changeRecAction(string msg) {
      aReceive.Text = msg;
      if(msg.Length > 0) {
        recClear.IsEnabled = true;
      }
    }

    private void unlisten() {
      if (recTh != null) {
        try {
          recTh.Abort();
        } catch(Exception err){
          MessageBox.Show(err.ToString());
        } finally {
          recTh = null;
        }
      }
    }

    private void closePort() {
      client.closeClient();
    }

    private void sendMsg(object sender, RoutedEventArgs e) {
      if (!client.checkLocalClient()) {
        MessageBox.Show("Udp端口未打开, 尝试重新Bind端口");
        return;
      }
      var ip = xRemoteIP.Text.Trim();
      var portStr = xRemotePort.Text.Trim();
      var msg = aSend.Text.Trim();
      try {
        int n = Convert.ToInt32(portStr);
        client.sendMsg(msg, ip, n);
        xTip.Text = "[√]Send succ!";
      } catch (FormatException) {
        xTip.Text = "[×]Send fail!";
        MessageBox.Show("IP或者Port格式错误, 请检查输入");
      } catch(SocketException) {
        xTip.Text = "[×]Send fail!";
        MessageBox.Show("发送失败, IP解析错误");
      } catch (Exception err) {
        xTip.Text = "[×]Send fail!";
        MessageBox.Show(err.ToString());
      }
    }

    private void clearRec(object sender, RoutedEventArgs e) {
      aReceive.Text = "";
      recClear.IsEnabled = false;
    }

    private void disposeClient(object sender, System.ComponentModel.CancelEventArgs e) {
      if(recTh != null) {
        unlisten();
      }
      if (client != null) {
        client.closeClient();
      }
    }

  }// class
}
