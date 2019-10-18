using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient.Client {
    class Client {
        private static IPEndPoint server;
        private static UdpClient localClient;

        public Client connectServer(String ip, Int32 port) {
            // 服务端IP
            server = new IPEndPoint(IPAddress.Parse(ip), port);
            // 绑定本地ip
            localClient = new UdpClient(8081);
            return this;
        }
    }
}
