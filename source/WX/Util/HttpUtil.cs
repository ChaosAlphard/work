using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Util {
    public static class HttpUtil {
        public static IRestResponse<T> sendGet<T>(string domain, string url) where T : new() {
            var client = new RestClient(domain);
            var req = new RestRequest(url, Method.GET);
            req.AddHeader("content-type", "application/json");

            IRestResponse<T> res = client.Execute<T>(req);
            return res;
        }

        public static IRestResponse<T> sendPost<T>(string domain, string url) where T : new() {
            var client = new RestClient(domain);
            var req = new RestRequest(url, Method.POST);
            req.AddHeader("content-type", "application/json");

            IRestResponse<T> res = client.Execute<T>(req);
            return res;
        }

        public static IRestResponse stringResult(string domain, string url) {
            var client = new RestClient(domain);
            var req = new RestRequest(url, Method.GET);
            req.AddHeader("content-type", "application/json");

            IRestResponse res = client.Execute(req);
            return res;
        }
    }
}
