using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient {
  class Program {
    static void Main(string[] args) {
      var client = new Client()
        .initSendClient(8081);

      while(true) {
        var line = Console.ReadLine();
        if(line.Equals("exit")) {
          break;
        }

        client.sendMsg(line, "127.0.0.1", 8080);
      }
    }
  }
}
