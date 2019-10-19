using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace SerialPortDemo {
  public partial class BaseForm : Form {

    // 串口对象
    private SerialPort port = new SerialPort();
    // msg
    // private StringBuilder sb = new StringBuilder();
    
    // bool isOpen, // 开启标识
    //   isSetProperty, // 属性设置
    //   isHex, // 十六进制显示
    //   isFileOpen = false;
    // string RecvDataText = null;

    // 主窗体加载
    public void onLoad(object sender, EventArgs e) {
      SPHandler.updateSerialPortList(_comCbx);
    }

    // 打开串口
    private void OpenSPort(object sender, EventArgs e) {
      if (!port.IsOpen) {
        // 串口名
        port.PortName = _comCbx.Text.Trim();
        // 波特率
        port.BaudRate = Convert.ToInt32(_botCbx.Text.Trim());
        // 数据位
        port.DataBits = Convert.ToInt32(_dataCbx.Text.Trim());
        // 停止位
        port.StopBits = this.getStopBits(_stopCbx.Text.Trim());
        // 无超时时间
        port.ReadTimeout = -1;
        // 奇偶效验
        port.Parity = Parity.Even;
        // 协议
        port.Handshake = Handshake.None;
        Console.WriteLine(port.PortName);
        Console.WriteLine(port.BaudRate);
        Console.WriteLine(port.DataBits);
        Console.WriteLine(port.StopBits);
        Console.WriteLine(port.ReadTimeout);
        Console.WriteLine(port.Handshake);

        try {
          port.Open();
          _openPort.Text = "关闭串口";
          MessageBox.Show("打开成功");
          _botCbx.Enabled = _comCbx.Enabled
                      = _dataCbx.Enabled
                      = _stopCbx.Enabled
                      = false;
          sendInput.Enabled = dataSend.Enabled = true;
          port.DataReceived += this.onDataReceived();
        } catch {
          MessageBox.Show("打开失败");
        }
      } else {
        try {
          port.Close();
          _openPort.Text = "打开串口";
          MessageBox.Show("关闭成功");
          _botCbx.Enabled = _comCbx.Enabled
                      = _dataCbx.Enabled
                      = _stopCbx.Enabled
                      = true;
          sendInput.Enabled = dataSend.Enabled = false;
          port.DataReceived -= this.onDataReceived();
        } catch {
          MessageBox.Show("关闭失败");
        }
      }
    }

    private void temp(object sender, EventArgs e) {
      Console.WriteLine("data");
      var sp = (SerialPort)sender;
      string data = sp.ReadExisting();
      Console.WriteLine($"Data: ${data}");
    }

    // 发送数据
    private void SubmitButtonClick(object sender, EventArgs e) {
      if (!port.IsOpen) {
        MessageBox.Show("ERR:\n串口没有打开");
        return;
      }
      var dataToSend = sendInput.Text.Trim();
      if (dataToSend == "") {
        MessageBox.Show("INFO:\n数据不能为空");
        return;
      }
      Console.WriteLine($"send: {dataToSend}");
      port.Write(dataToSend);
    }

    private StopBits getStopBits(string stopStr) {
      switch(stopStr) {
        case "1":
          return StopBits.One;
        case "1.5":
          return StopBits.OnePointFive;
        case "2":
          return StopBits.Two;
        default: 
          return StopBits.None;
      }
    }

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

    public BaseForm() {
      InitializeComponent();
    }

    private void onSPUpdate(object sender, EventArgs e) {
        SPHandler.updateSerialPortList()
    }
  }// class
}
