using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortDemo {
  public partial class BaseForm : Form {

    // 串口对象
    private SerialPort port = new SerialPort();
    // msg
    private StringBuilder sb = new StringBuilder();
    
    bool isOpen, // 开启标识
      isSetProperty, // 属性设置
      isHex, // 十六进制显示
      isFileOpen = false;
    string RecvDataText = null;

    private void OpenSPort(object sender, EventArgs e) {
      if (!port.IsOpen) {
        // 串口名
        port.PortName = sPortComboBox.Text;
        // 波特率
        port.BaudRate = Convert.ToInt32(bot.Text);
        // 数据位
        port.DataBits = Convert.ToInt32(dataBit.Text);
        // 停止位
        port.StopBits = StopBits.One;
        // 无超时时间
        port.ReadTimeout = -1;
        // 无协议
        port.Handshake = Handshake.None;
      }
    }

    private void SubmitButtonClick(object sender, EventArgs e) {
      Console.WriteLine($"{this.textBox1.Text}===================================");
      Console.WriteLine(sPortComboBox.Text);
      if(!port.IsOpen) {
        port.PortName = sPortComboBox.Text;
        port.BaudRate = Convert.ToInt32(bot.Text);
      }
    }


    public BaseForm() {
      InitializeComponent();
    }

    private void formLoad(object sender, EventArgs e) {
      this.MaximizeBox = false;
      this.MaximumSize = this.Size;
      this.MinimumSize = this.Size;

      for(int i=0; i<10; i++) {
        // cbxComPort.Items.Add($"COM{i+1}({i})");
      }
    }
  }
}
