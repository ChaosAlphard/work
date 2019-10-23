using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Udp {
  class Client {
    private UdpClient client;

    public Client() {
      client = new UdpClient();
    }

    public Client(int port) {
      client = new UdpClient(port);
    }

    /**查看是否创建Udp服务
     */
    private bool hasLocalEP() {
      if(client!=null&&client.Client!=null) {
        return client.Client.LocalEndPoint != null;
      } else {
        return false;
      }
    }

    public bool checkLocalClient() {
      return hasLocalEP();
    }

    public void sendMsg(string msg, string ip, int port) {
      var remoteIP = IPAddress.Parse(ip);
      var iep = new IPEndPoint(remoteIP, port);
      var bytes = Encoding.UTF8.GetBytes(msg);
      client.Send(bytes, bytes.Length, iep);
    }

    public void sendMsg(byte[] bytes, IPEndPoint iep) {
      client.Send(bytes, bytes.Length, iep);
    }

    /**
    public Client startRecDataFromAnyIP() {
      new Thread(receiveDataThread) {
        IsBackground = true
      }.Start();

      return this;
    }
    private void receiveDataThread() {
      var iep = new IPEndPoint(IPAddress.Any, 0);

      while(true) {
        byte[] recByte = client.Receive(ref iep);
        string recStr = Encoding.UTF8.GetString(recByte);
      }
    }
    */

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
