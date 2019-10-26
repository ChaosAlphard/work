using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using Udp.Handler;

namespace Udp {
  /// <summary>
  /// MainWindow.xaml 的交互逻辑
  /// </summary>
  public partial class MainWindow : Window {
    private delegate void changeReceive(string msg);

    private Client client = new Client();

    private bool __isOpen = false;
    private Thread recTh = null;
    private string recStr = "";

    private bool isOpen {
      get {
        return __isOpen;
      }
      set {
        __isOpen = value;
        aSend.IsReadOnly = !value;
        xLocalPort.IsReadOnly = xSendBtn.IsEnabled = value;
        if(value) {
          xBindLocalPortBtn.Content = "Unbind";
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
          xTip.Text = "[√]UnBind succ!";
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
      recStr = "";

      var reg = new Regex(@"^ \$\{\|\|([0-9A-F]*)\|\|\}\-((.|\n)*)$");

      while (true) {
        byte[] recByte = new byte[0];
        try {
          recByte = client.getClient().Receive(ref iep);
        } catch (ThreadAbortException) {
          //
        } catch (SocketException) {
          aReceive.Dispatcher.Invoke(
            new changeReceive(changeTipAction),
            "[×]Send fail!");
          //xTip.Text = "发送失败";
          continue;
        } catch (Exception ex) {
          MessageBox.Show(ex.ToString());
        }

        string strCol = Util.byte2Str(recByte);
        var res = reg.Match(strCol);
        var sha = res.Groups[1].ToString();
        var str = res.Groups[2].ToString();

        if(!Util.getSha1(Util.str2Byte(str)).Equals(sha)) {
          // MessageBox.Show("文本校验失败, 数据丢失");
          recStr += "[校验失败]";
          // continue;
        }

        var _ip = iep.Address.ToString();
        var _port = iep.Port;
        recStr += $"来自[{_ip}:{_port}]的消息:\n{str}\n";
        // aReceive.Text = recStr;
        aReceive.Dispatcher.Invoke(
          new changeReceive(changeRecAction),
          recStr);
      }

    }

    private void changeRecAction(string msg) {
      aReceive.Text = msg;
      if(msg.Length > 0) {
        recClear.IsEnabled = true;
      }
    }

    private void changeTipAction(string msg) {
      xTip.Text = msg;
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
      if(msg == "") {
        xTip.Text = "[!]数据不能为空";
        return;
      }
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
      recStr = "";
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
