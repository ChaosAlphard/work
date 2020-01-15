using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    public class User {
        public String openid { get; set; }
        public String name { get; set; }
        public String avatar { get; set; }
        public String unionid { get; set; }
        public Int32 level { get; set; }
    }
}
