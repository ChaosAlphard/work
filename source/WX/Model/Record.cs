using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    public class Record {
        public Int32 id { get; set; }
        public String openid { get; set; }
        public String name { get; set; }
        public String message { get; set; }
        public String time { get; set; }
        public Int32 judge { get; set; }

        public bool isValid() {
            return (
                openid!=null&&
                name!=null&&
                message!=null
            );
        }
    }
}
