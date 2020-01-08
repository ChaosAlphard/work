using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
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
