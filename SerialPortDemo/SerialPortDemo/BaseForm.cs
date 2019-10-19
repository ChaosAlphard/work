using Microsoft.Win32;
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
    
    // bool isOpen, // 开启标识
    //   isSetProperty, // 属性设置
    //   isHex, // 十六进制显示
    //   isFileOpen = false;
    // string RecvDataText = null;

    // 主窗体加载
    public void onLoad(object sender, EventArgs e) {
      // 扫描已存在的串口
      RegistryKey keyCom = Registry
        .LocalMachine
        .OpenSubKey("Hardware\\DeviceMap\\SerialComm");

      if(keyCom != null) {
        string[] subKeys = keyCom.GetValueNames();
        // 清除串口
        sPortComboBox.Items.Clear();
        foreach(string sName in subKeys) {
          // 获取串口名
          string value = keyCom.GetValue(sName)?.ToString() ?? "Fail";
          // 添加串口
          sPortComboBox.Items.Add(value);
        }
        if(sPortComboBox.Items.Count > 0) {
          // 选择索引 设置为1
          sPortComboBox.SelectedIndex = 1;
        }

      }
    }

    // 打开串口
    private void OpenSPort(object sender, EventArgs e) {
      if (!port.IsOpen) {
        // 串口名
        port.PortName = sPortComboBox.Text.Trim();
        // 波特率
        port.BaudRate = Convert.ToInt32(bot.Text.Trim());
        // 数据位
        port.DataBits = Convert.ToInt32(dataBox.Text.Trim());
        // 停止位
        port.StopBits = this.getStopBits(stopBox.Text.Trim());
        // 无超时时间
        port.ReadTimeout = -1;
        // 无协议
        port.Handshake = Handshake.None;

        try {
          port.Open();
          Open.Text = "关闭串口";
          MessageBox.Show("打开成功");
          bot.Enabled = sPortComboBox.Enabled
                      = dataBox.Enabled
                      = stopBox.Enabled
                      = false;
          Submit.Enabled = true;
          port.DataReceived += this.onDataReceived();
        } catch {
          MessageBox.Show("打开失败");
        }
      } else {
        try {
          port.Close();
          Open.Text = "打开串口";
          MessageBox.Show("关闭成功");
          bot.Enabled = sPortComboBox.Enabled
                      = dataBox.Enabled
                      = stopBox.Enabled
                      = true;
          Submit.Enabled = false;
          port.DataReceived -= this.onDataReceived();
        } catch {
          MessageBox.Show("关闭失败");
        }
      }
    }

    // 发送数据
    private void SubmitButtonClick(object sender, EventArgs e) {
      Console.WriteLine($"{this.textBox1.Text}===================================");
      Console.WriteLine(sPortComboBox.Text);
      if (!port.IsOpen) {
        port.PortName = sPortComboBox.Text;
        port.BaudRate = Convert.ToInt32(bot.Text);
      }
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
          try {
            var strbud = new StringBuilder();
            while(port.BytesToRead > 0) {
              var sch = port.ReadByte().ToString();
              Console.WriteLine(sch);
              strbud.Append(port.ReadByte());
            }
            Console.WriteLine(strbud.ToString());
          } catch(Exception err) {
            MessageBox.Show($"在读取时出现错误:\n{err}");
          }
        }
      );
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
