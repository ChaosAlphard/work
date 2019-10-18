using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient {
  class Client {

    UdpClient sendClient;

    public Client initSendClient(Int32 port) {
      sendClient = new UdpClient(port);
      return this;
    }
    public Client sendMsg(string msg, String ip, Int32 port) {
      
      var bytes = Encoding.UTF8.GetBytes(msg);

      var remoteIP = IPAddress.Parse(ip);
      var ep = new IPEndPoint(remoteIP, port);

      try {
        sendClient.Send(bytes, bytes.Length, ep);
        //
      } catch(Exception e) {
        Console.WriteLine(e);
        return this;
      }

      return this;
    }

  }
}
