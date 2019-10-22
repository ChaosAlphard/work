using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortDemo.Handler {
  class StrHandler {
    // hex-str to bytes
    public static byte[] hex2Bytes(string hex) {
      hex = hex.Replace(" ", "").Replace("-","");
      byte[] raw = new byte[hex.Length / 2];
      for(int i=0; i<raw.Length; i++) {
        try {
          raw[i] = Convert.ToByte(
            hex.Substring(i * 2, 2), 16
          );
        } catch(FormatException) {
          MessageBox.Show("解析错误, 转换格式失败, 请检查输入");
          break;
        } catch(Exception e) {
          MessageBox.Show(e.ToString());
          break;
        }
      }
      return raw;
    }

    public static byte[] hex2BytesV2(string hex) {
      string[] tmp = hex.Trim().Split(' ');
      byte[] buff = new byte[tmp.Length];
      for (int i = 0; i < buff.Length; i++) {
        buff[i] = Convert.ToByte(tmp[i], 16);
      }
      return buff;
    }

    // hex-str to string
    public static string hex2Str(string hex) {
      try {
        return Encoding.Default
        .GetString(hex2Bytes(hex));
      } catch (FormatException) {
        MessageBox.Show("解析错误, 转换格式失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return "";
    }

    // str to bytes
    public static byte[] str2Byte(string str) {
      try {
        return Encoding.Default.GetBytes(str);
      } catch (FormatException) {
        MessageBox.Show("String转Byte失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return new byte[0];
    }

    // str to hex-str
    public static string str2Hex(string str) {
      try {
        return BitConverter.ToString(str2Byte(str));
      } catch (FormatException) {
        MessageBox.Show("解析错误, 转换格式失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return "";
    }

    public static string str2HexV2(string str) {
      try {
        return BitConverter.ToString(str2Byte(str)).Replace("-", " ");
      } catch (FormatException) {
        MessageBox.Show("解析错误, 转换格式失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return "";
    }

    //bytes to hex-str
    public static string byte2Hex(byte[] bytes) {
      try {
        return BitConverter.ToString(bytes);
      } catch (FormatException) {
        MessageBox.Show("Byte转Hex失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return "";
    }

    public static string byte2HexV2(byte[] bytes) {
      try {
        return BitConverter.ToString(bytes).Replace("-"," ");
      } catch (FormatException) {
        MessageBox.Show("Byte转Hex失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return "";
    }

    //bytes to string
    public static string byte2Str(byte[] bytes) {
      try {
        return Encoding.Default.GetString(bytes);
      } catch (FormatException) {
        MessageBox.Show("Byte转String失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return "";
    }
  }// class
}
