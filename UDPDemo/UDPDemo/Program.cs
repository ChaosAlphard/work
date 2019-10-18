using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPDemo {
  class Program {
    static void Main(string[] args) {
      new Net().createServer()
        .bindIPHost("127.0.0.1", 8080)
        .run();

      Console.ReadKey();
    }
  }
}