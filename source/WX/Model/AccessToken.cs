using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    public class AccessToken {
        public String access_token { get; set; }
        public Int32 expires_in { get; set; }
    }
}
