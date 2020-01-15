using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    // 申请记录
    public class Record {
        public String id { get; set; }
        public String openid { get; set; }
        public String name { get; set; }
        public String message { get; set; }
        public String time { get; set; }
        public Int32 judge { get; set; }

        public bool isValid() {
            return (
                id!=null&&
                openid!=null&&
                name!=null&&
                message!=null
            );
        }
    }
}
