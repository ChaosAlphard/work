using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    // 订阅消息的结果
    public class SubMsgResult {
        public Int32 errcode { get; set; }
        public String errmsg { get; set; }
        public Object data { get; set; }
    }
}
