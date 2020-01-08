using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WX.Cache;
using WX.Dto;
using WX.Model;
using WX.Service;
using WX.Util;
using RestSharp;

namespace WX.Service {
    public class WXApiImpl : WXApiService {
        private static readonly string domain = "https://api.weixin.qq.com";

        public T requireSubMsg<T, O>(
                String openid,
                String templateid,
                O msgData,
                String page = null
        ) where T : new()
        where O : new() {
            var subMsg = new SubscribeMessage<O>();
            subMsg.touser = openid;
            subMsg.template_id = templateid;
            subMsg.data = msgData;
            subMsg.page = page;

            Console.WriteLine(subMsg.ToString());
            IRestResponse<T> response = HttpUtil.sendPost<T>(domain, getSubUrl(), subMsg);

            return response.Data;
        }

        private string getSubUrl() {
            return $"/cgi-bin/message/subscribe/send?access_token={AccessTokenCache.getAccessTokenCache()}";
        }
    }
}
