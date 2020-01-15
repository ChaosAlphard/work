using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    // AccessToken 临时凭证
    public class AccessToken {
        public String access_token { get; set; }
        public Int32 expires_in { get; set; }
    }
}
