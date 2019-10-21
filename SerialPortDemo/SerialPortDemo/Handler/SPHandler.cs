using System;
using System.IO.Ports;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SerialPortDemo.Handler {
  static class SPHandler {

    public static bool updateSerialPortList(ComboBox cbx) {
      // 扫描已存在的串口
      RegistryKey com = Registry
        .LocalMachine
        .OpenSubKey("Hardware\\DeviceMap\\SerialComm");

      if(com == null) {
        MessageBox.Show("在本机上没有发现串口");
        return false;
      }

      string[] subKeys = com.GetValueNames();
      // 清除串口
      cbx.Items.Clear();
      foreach(string sName in subKeys) {
        // 获取串口名
        string value = com.GetValue(sName)?.ToString() ?? "NotFound";
        // 添加串口
        cbx.Items.Add(value);

        if(cbx.Items.Count > 0) {
          // 选择索引 设置为0
          cbx.SelectedIndex = 0;
        } else {
          MessageBox.Show("串口设置失败，请手动设置");
        }
      }
      return true;
    }

    public static bool openSPort(
      SerialPort port, string name,
      int baudRate, int dataBits,
      StopBits stopBits, Parity parity,
      Handshake handshake, int timeout,
      Label tip
    ) {
      port.PortName = name;
      port.BaudRate = baudRate;
      port.DataBits = dataBits;
      port.StopBits = stopBits;
      port.Parity = parity;
      port.Handshake = handshake;
      port.ReadTimeout = timeout;

      try {
        port.Open();
        tip.Text = "打开成功";
        
        return true;
      } catch(Exception e) {
        tip.Text = "打开失败";
        MessageBox.Show(e.ToString());
        return false;
      }
    }

    public static bool closeSPort(SerialPort port, Label tip) {
      try {
        port.Close();
        tip.Text = "关闭成功";
        return true;
      } catch {
        tip.Text = "关闭失败";
        return false;
      }
    }

    public static StopBits getStopBits(int stopSel) {
      switch(stopSel) {
        case 1:
          return StopBits.Two;
        case 2: // 暂不启用
          return StopBits.OnePointFive;
        default:
          return StopBits.One;
      }
    }

    public static Parity getParity(int parSel) {
      switch(parSel) {
        case 1:
          return Parity.Odd;
        case 2:
          return Parity.Even;
        case 3:
          return Parity.Mark;
        case 4:
          return Parity.Space;
        default:
          return Parity.None;
      }
    }

    public static Handshake getHandshake(int hanSel) {
      switch(hanSel) {
        case 1:
          return Handshake.XOnXOff;
        case 2: // 暂不启用
          return Handshake.RequestToSend;
        case 3: // 暂不启用
          return Handshake.RequestToSendXOnXOff;
        default:
          return Handshake.None;
      }
    }

  }// class
}
