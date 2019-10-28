using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Udp.Handler {
  static class Util {

    public static IPEndPoint getIPEndPoint(string ip, int port) {
      var remoteIP = IPAddress.Parse(ip);
      return new IPEndPoint(remoteIP, port);
    }

    public static byte[] str2Byte(string str) {
      try {
        return Encoding.UTF8.GetBytes(str);
      } catch (FormatException) {
        MessageBox.Show("String转Byte失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return new byte[0];
    }

    public static string byte2Str(byte[] bytes) {
      try {
        return Encoding.UTF8.GetString(bytes);
      } catch (FormatException) {
        MessageBox.Show("Byte转String失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return "";
    }

    public static byte[] hex2Bytes(string hex) {
      hex = hex.Replace(" ", "").Replace("-", "");
      byte[] raw = new byte[hex.Length / 2];
      for (int i = 0; i < raw.Length; i++) {
        try {
          raw[i] = Convert.ToByte(
            hex.Substring(i * 2, 2), 16
          );
        } catch (FormatException) {
          MessageBox.Show("解析错误, 转换格式失败, 请检查输入");
          break;
        } catch (Exception e) {
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

    public static string hex2Str(string hex) {
      try {
        return Encoding.UTF8
        .GetString(hex2Bytes(hex));
      } catch (FormatException) {
        MessageBox.Show("解析错误, 转换格式失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return "";
    }

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
        return BitConverter.ToString(bytes).Replace("-", " ");
      } catch (FormatException) {
        MessageBox.Show("Byte转Hex失败, 请检查输入");
      } catch (Exception e) {
        MessageBox.Show(e.ToString());
      }
      return "";
    }

    public static string getSha1(byte[] bytes) {
      byte[] sha = SHA1.Create().ComputeHash(bytes);

      var builder = new StringBuilder();
      for (int i = 0; i < sha.Length; i++) {
        builder.Append(sha[i].ToString("X2"));
      }

      return builder.ToString();
    }

  }// class
}
