using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WX.Model;
using WX.Util;

namespace WX.Cache {
    public class AccessTokenCache {
        private static string idAndSecret = AppSettings.getIdAndSecret();

        private static string domain = "https://api.weixin.qq.com";
        private static string url = $"/cgi-bin/token?grant_type=client_credential&{idAndSecret}";

        private static string accessToken = null;
        private static long expiresTime = 0;

        /**
         * 将AccessToken暴露给外部
         * @return (String) AccessToken
         */
        public static String getAccessTokenCache() {
            if(AccessTokenCache.accessToken!=null&&AccessTokenCache.isTokenValid()) {
                return AccessTokenCache.accessToken;
            }
            AccessToken token = AccessTokenCache.getAccessToken();
            AccessTokenCache.setCache(token);

            return AccessTokenCache.accessToken;
        }

        /**
         * 从腾讯API获取AccessToken
         * @return AccessToken
         */
        private static AccessToken getAccessToken() {
            return HttpUtil.sendGet<AccessToken>(domain, url).Data;
        }

        /**
         * 将获取到的token缓存起来
         * @param token AccessToken
         */
        private static void setCache(AccessToken token) {
            AccessTokenCache.accessToken = token.access_token;
            int expires = token.expires_in - 600;   //10*60s 提前10min申请accessToken
            long current = DateUtil.getCurrentSecond();
            AccessTokenCache.expiresTime = current + expires;

            string validTime = DateUtil.timeSecond2Str(current);
            string invalidTime = DateUtil.timeSecond2Str(AccessTokenCache.expiresTime);
            Console.WriteLine($"[{validTime}]: 获取Token成功, 失效时间: [{invalidTime}]");
        }

        /**
         * 判断Token是否有效
         * @return 是(true)否(false)有效
         */
        private static bool isTokenValid() {
            if(AccessTokenCache.expiresTime <= 0L) {
                return false;
            }
            bool isValid = AccessTokenCache.expiresTime > DateUtil.getCurrentSecond();
            if(isValid) {
                Console.WriteLine("Token有效");
            } else {
                Console.WriteLine("Token失效");
            }
            return isValid;
        }
    }
}
