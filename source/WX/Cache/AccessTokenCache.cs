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

        private string accessToken = null;
        private long expiresTime = 0;

        /**
         * 将AccessToken暴露给外部
         * @return (String) AccessToken
         */
        public String getAccessTokenCache() {
            if(this.accessToken!=null&&this.isTokenValid()) {
                return this.accessToken;
            }
            AccessToken token = this.getAccessToken();
            this.setCache(token);

            return this.accessToken;
        }

        /**
         * 从腾讯API获取AccessToken
         * @return AccessToken
         */
        private AccessToken getAccessToken() {
            return HttpUtil.sendGet<AccessToken>(domain, url).Data;
        }

        /**
         * 将获取到的token缓存起来
         * @param token AccessToken
         */
        private void setCache(AccessToken token) {
            this.accessToken = token.access_token;
            int expires = token.expires_in - 600;   //10*60s 提前10min申请accessToken
            long current = DateUtil.getCurrentSecond();
            this.expiresTime = current + expires;
            Console.WriteLine($"[{current}]获取Token成功, 过期时间: [{this.expiresTime}]");
        }

        /**
         * 判断Token是否有效
         * @return 是(true)否(false)有效
         */
        private bool isTokenValid() {
            if(this.expiresTime <= 0L) {
                return false;
            }
            bool isValid = this.expiresTime > DateUtil.getCurrentSecond();
            return isValid;
        }
    }
}
