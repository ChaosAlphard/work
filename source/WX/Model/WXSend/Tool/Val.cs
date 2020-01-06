using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    public class Val {
        public String value { get; set; }

        public static Val Of(string value) {
            return new Val(value);
        }

        public Val() { }

        public Val(string value) {
            this.value = value;
        }
    }
}
