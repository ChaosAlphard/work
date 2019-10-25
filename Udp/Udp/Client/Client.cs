using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Udp.Handler;

namespace Udp {
  class Client {
    private UdpClient client;

    public Client() {
      client = new UdpClient();
    }

    public Client(int port) {
      client = new UdpClient(port);
    }

    /* 查看是否创建Udp服务 */
    public bool checkLocalClient() {
      return client?.Client?.LocalEndPoint != null;
    }

    public void sendMsg(string msg, string ip, int port) {
      var remoteIP = IPAddress.Parse(ip);
      var iep = new IPEndPoint(remoteIP, port);

      string sha = Util.getSha1(Util.str2Byte(msg));
      var bytes = Util.str2Byte(" ${||"+sha+"||}-"+msg);

      client.Send(bytes, bytes.Length, iep);
    }

    public void closeClient() {
      client.Close();
      client.Dispose();
    }

    public void discardClient() {
      client = null;
    }

    public UdpClient getClient() {
      return client;
    }
  }
}
