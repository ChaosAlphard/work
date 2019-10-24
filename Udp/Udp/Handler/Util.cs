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
