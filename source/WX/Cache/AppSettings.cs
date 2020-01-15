using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Cache {
    public static class AppSettings {
        // AppID: App标识，每一个APP对应一个ID
        private static readonly string appId = "wx8a3e8bf03782398e";
        // AppSecret: 秘钥(?) 通行标识
        private static readonly string appSecret = "2c41233c37b9de2d2eb31af2e75964f8";
        // url请求通常需要二者一起
        private static string idAndSecret = $"appid={appId}&secret={appSecret}";

        public static string getId() {
            return appId;
        }

        public static string getSecret() {
            return appSecret;
        }

        public static string getIdAndSecret() {
            return idAndSecret;
        }
    }
}
