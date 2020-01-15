using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    // 转换为符合微信要求的格式
    // key: {
    //   value: 'value'
    // }
    public class Val {
        public String value { get; set; }

        public static Val Of(string value) {
            Val val = new Val();
            val.value = value;
            return val;
        }

        public override string ToString() {
            return $"Val='{value}'";
        }
    }
}
