using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Cache {
    public class AccessTokenCache {
        private static string idAndSecret = AppSettings.getIdAndSecret();
        private static string
            getTokenUrl = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&{idAndSecret}";

        private string accessToken = null;
        private long expiresTime = 0;
    }
}
