using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace SerialPortDemo {
  public partial class BaseForm : Form {

    // 串口对象
    private SerialPort port = new SerialPort();
    // msg
    private StringBuilder recStr = new StringBuilder();

    bool sendIsHex, recIsHex = false;

    public BaseForm() {
      InitializeComponent();
    }

    // 主窗体加载
    public void onLoad(object sender, EventArgs e) {
      SPHandler.updateSerialPortList(_comCbx);
    }

    // 更新
    private void onSPUpdate(object sender, EventArgs e) {
      SPHandler.updateSerialPortList(_comCbx);
    }

    // 打开串口
    private void OpenSPort(object sender, EventArgs e) {
      if(!port.IsOpen) {
        SPHandler.openSPort(
          port,
          _comCbx.Text.Trim(),  // 串口名
          Convert.ToInt32(_botCbx.Text.Trim()), // 波特率
          Convert.ToInt32(_dataCbx.Text.Trim()),  // 数据位
          SPHandler.getStopBits(_stopCbx.Text.Trim()), // 停止位                    
          Parity.None,  // 奇偶效验
          Handshake.None,  // 协议
          -1 // 无超时时间
        );
        _openPort.Text = "关闭串口";
        _botCbx.Enabled = _comCbx.Enabled
                    = _dataCbx.Enabled
                    = _stopCbx.Enabled
                    = false;
        _send_input.Enabled = _send_submit.Enabled = true;
        port.DataReceived += this.onDataReceived();
      } else {
        SPHandler.closeSPort(port);
        _openPort.Text = "打开串口";
        _botCbx.Enabled = _comCbx.Enabled
                    = _dataCbx.Enabled
                    = _stopCbx.Enabled
                    = true;
        _send_input.Enabled = _send_submit.Enabled = false;
        port.DataReceived -= this.onDataReceived();
      }
    }

    // 发送数据
    private void sendData(object sender, EventArgs e) {
      if(!port.IsOpen) {
        MessageBox.Show("ERR:\n串口没有打开");
        return;
      }
      var dataToSend = _send_input.Text.Trim();
      if(dataToSend == "") {
        MessageBox.Show("INFO:\n数据不能为空");
        return;
      }
      Console.WriteLine($"send: {dataToSend}");
      port.Write(dataToSend);
    }

////////////////////////////////////////////////////////////////////////////////
////Event///////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
    private SerialDataReceivedEventHandler onDataReceived() {
      return new SerialDataReceivedEventHandler(
        (sender, e) => {
          Console.WriteLine("data");
          var sp = (SerialPort)sender;
          string data = sp.ReadExisting();
          receiveArea.Text = $"Data: ${data}";
          // try {
          //   var strbud = new StringBuilder();
          //   while(port.BytesToRead > 0) {
          //     var sch = port.ReadByte().ToString();
          //     Console.WriteLine(sch);
          //     strbud.Append(port.ReadByte());
          //   }
          //   Console.WriteLine(strbud.ToString());
          // } catch(Exception err) {
          //   MessageBox.Show($"在读取时出现错误:\n{err}");
          // }
        }
      );
    }
  }// class
}
