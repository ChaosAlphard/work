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
      hex = hex.Replace("-", "");
      byte[] raw = new byte[hex.Length / 2];
      for(int i=0; i<raw.Length; i++) {
        try {
          raw[i] = Convert.ToByte(
            hex.Substring(i * 2, 2), 16
          );
        } catch(Exception e) {
          MessageBox.Show(e.ToString());
        }
      }
      return raw;
    }

    // hex-str to string
    public static string hex2Str(string hex) {
      return Encoding.Default
        .GetString(hex2Bytes(hex));
    }

    // str to bytes
    public static byte[] str2Byte(string str) {
      return Encoding.Default.GetBytes(str);
    }

    // str to hex-str
    public static string str2Hex(string str) {
      return BitConverter.ToString(str2Byte(str));
    }

    //bytes to hex-str
    public static string byte2Hex(byte[] bytes) {
      return BitConverter.ToString(bytes);
    }
  }// class
}
