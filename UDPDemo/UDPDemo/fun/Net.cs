using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDPDemo {
    class Net {
        private static Socket udpServer;

        /**
         * 创建udpServer
         */
        public Net createServer() {
            if(udpServer != null) {
                throw new Exception("udpServer已被实例化");
            }
            udpServer = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp
            );

            Console.WriteLine("创建udpServer...");
            return this;
        }

        /**
         * 绑定IP和端口
         */
        public Net bindIPHost(String ip, Int32 port) {
            udpServer.Bind(
                new IPEndPoint(
                    IPAddress.Parse(ip),
                    port
                )
            );

            Console.WriteLine($"绑定IP{ip}:{port}...");
            return this;
        }

        /**
         * 开启一个线程
         */
        public Net run() {
            new Thread(onMessage) {
                IsBackground = true
            }.Start();

            Console.WriteLine("开始监听...");
            return this;
        }

        // 接受数据
        static void onMessage() {
            while(true) {
                EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = new byte[1024];

                //这个方法会把数据的来源(ip:port)放到第二个参数上
                int length = udpServer
                    .ReceiveFrom(data, ref remoteEndPoint);

                string message = Encoding.UTF8.GetString(data, 0, length);
                string _ip = (remoteEndPoint as IPEndPoint).Address.ToString();
                string _port = (remoteEndPoint as IPEndPoint).Port.ToString();
                Console.WriteLine($"来自[{_ip}:{_port}]的消息: \n{message}");
            }
        }
    }
}
