using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    public class RemindMsg {
        public Val thing1 { get; set; }
        public Val date2 { get; set; }

        public override string ToString() {
            return $"RemindMsg[" +
                $"thing1:'{thing1?.ToString()}'," +
                $"date2:'{date2?.ToString()}'" +
                $"]";
        }
    }
}
