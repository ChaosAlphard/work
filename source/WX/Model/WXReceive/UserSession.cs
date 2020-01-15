using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    // 用户的openid和sessionkey
    public class UserSession {
        public String session_key { get; set; }
        public String openid { get; set; }
    }
}
