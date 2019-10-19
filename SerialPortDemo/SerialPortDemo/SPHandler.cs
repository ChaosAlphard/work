using System;
using System.IO.Ports;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SerialPortDemo {
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
      Handshake handshake, int timeout
    ) {
      port.PortName = name;
      port.BaudRate = baudRate;
      port.DataBits = dataBits;
      port.StopBits = stopBits;
      port.ReadTimeout = -1;
      port.Parity = parity;
      port.Handshake = handshake;

      Console.WriteLine("===========================");
      Console.WriteLine(port.PortName);
      Console.WriteLine(port.BaudRate);
      Console.WriteLine(port.DataBits);
      Console.WriteLine(port.StopBits);
      Console.WriteLine(port.ReadTimeout);
      Console.WriteLine(port.Parity);
      Console.WriteLine(port.Handshake);

      try {
        port.Open();
        MessageBox.Show("打开成功");
        return true;
      } catch {
        MessageBox.Show("打开失败");
        return false;
      }
    }

    public static bool closeSPort(SerialPort port) {
      try {
        port.Close();
        MessageBox.Show("关闭成功");
        return true;
      } catch {
        MessageBox.Show("关闭失败");
        return false;
      }
    }

    public static StopBits getStopBits(string stopStr) {
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

  }// class
}
