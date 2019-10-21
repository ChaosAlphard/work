using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortDemo.DTO {
  class Msg {
    public bool flag { get; set; }
    public string msg { get; set; }
    public Msg(bool flag, string msg) {
      this.flag = flag;
      this.msg = msg;
    }
  }
}
