﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    // 用户信息, unionid目前未使用
    public class User {
        public String openid { get; set; }
        public String name { get; set; }
        public String avatar { get; set; }
        public String unionid { get; set; }
        public Int32 level { get; set; }
    }
}
