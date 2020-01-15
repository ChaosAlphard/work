using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    // 订阅消息
    public class RemindMsg {
        public Val name1 { get; set; }
        public Val thing2 { get; set; }
        public Val time3 { get; set; }

        public override string ToString() {
            return $"RemindMsg["+
                $"name1:'{name1?.ToString()}',"+
                $"thing2:'{thing2?.ToString()}'"+
                $"time3:'{time3?.ToString()}'"+
                $"]";
        }
    }
}
