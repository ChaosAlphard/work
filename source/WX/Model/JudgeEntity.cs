using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    // 返回给申请者的结果
    public class JudgeEntity {
        public string id { get; set; }
        public string openid { get; set; }
        public string isAgree { get; set; }

        public bool isValid() {
            return (
                id != null &&
                openid != null &&
                isAgree != null
            );
        }
    }
}
