using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WX.Cache;
using WX.Dao;
using WX.Dto;
using WX.Model;
using WX.Util;

namespace WX.Service {
    public class UserImpl : UserService {
        private UserDao ud = new UserDao();
        private readonly string idAndSecret = AppSettings.getIdAndSecret();

        private readonly string domain = "https://api.weixin.qq.com";

        public VDto<UserSession> getSession(string code) {
            string url = $"/sns/jscode2session?{idAndSecret}&js_code={code}&grant_type=authorization_code";
            try {
                // 此Api返回string, 所以要用string接收后再转为json
                string res = HttpUtil.stringResult(domain, url).Content;
                UserSession user = JsonConvert.DeserializeObject<UserSession>(res);
                return VDto<UserSession>.OfModel(Status.GET_DATA_SUCCESS, user);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<UserSession>.Of(Status.GET_DATA_FAIL);
            }
        }

        // 大致流程基本一致:
        // 从dao中查到dataTable数据集，
        // 判断是否为null, 
        // 不为null则转换为实体类并返回
        public VDto<User> findAll() {
            var res = ud.queryAll();
            // res为null 说明dao方法中抛异常
            if(res == null) {
                return VDto<User>.Of(Status.SQL_ERROR);
            }
            try {
                // 捕获DataTable转EntityList的异常
                var lis = DBUtil.data2List(new List<User>(), res);
                return VDto<User>.OfData(Status.GET_DATA_SUCCESS, lis);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<User>.Of(Status.DATA_TO_LIST_FAIL);
            }
        }

        public VDto<User> findByOpenid(string openid) {
            User usr = null;
            try {
                usr = findUser(openid);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<User>.Of(Status.DATA_TO_MODEL_FAIL);
            }
            if(usr == null) {
                return VDto<User>.Of(Status.NOT_FOUND);
            } else {
                return VDto<User>.OfModel(Status.GET_DATA_SUCCESS, usr);
            }
        }

        public VDto<Int32> findLevelByOpenid(string openid) {
            var res = ud.findLevelByOpenid(openid);
            if(res == null) {
                return VDto<Int32>.OfModel(Status.SQL_ERROR, -1);
            }
            try {
                if(res.Rows.Count == 0) {
                    return VDto<Int32>.OfModel(Status.NOT_FOUND, -1);
                }
                return VDto<Int32>.OfModel(
                    Status.GET_DATA_SUCCESS,
                    Convert.ToInt32(res.Rows[0]["level"])
                );
            } catch(Exception e) {
                Console.WriteLine(e.StackTrace);
                return VDto<Int32>.OfModel(Status.SQL_ERROR, -1);
            }
        }

        public VDto<User> updateUser(string openid, string name, string avatar) {
            User usr;
            try {
                // 先查询用户
                usr = findUser(openid);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<User>.Of(Status.DATA_TO_MODEL_FAIL);
            }
            int res;
            // 不存在则新增，存在则更新
            if(usr == null) {
                res = ud.insertUser(openid, name, avatar);
            } else {
                res = ud.updateUser(openid, name, avatar);
            }
            // res代表受影响行数, 为-1 说明dao抛异常, 为0说明没有更新到
            if(res < 0) {
                return VDto<User>.Of(Status.SQL_ERROR);
            } else if(res == 0) {
                return VDto<User>.Of(Status.UPDATE_FAIL);
            } else {
                return VDto<User>.Of(Status.UPDATE_SUCCESS);
            }
        }

        private User findUser(string openid) {
            var res = ud.findByOpenid(openid);
            if(res == null) {
                return null;
            }
            return DBUtil.data2Model(new User(), res.Rows[0]);
        }

    }
}
