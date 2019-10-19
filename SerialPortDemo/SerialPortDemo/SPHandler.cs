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

  }// class
}
