using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WX.Dao;
using WX.Dto;
using WX.Model;
using WX.Util;

namespace WX.Service {
    public class UserImpl : UserService {
        UserDao ud = new UserDao();

        public VDto<User> findAll() {
            var res = ud.queryAll();
            if(res == null) {
                return VDto<User>.Of(Status.SQL_ERROR);
            }
            try {
                var lis = DBUtil.data2List(new List<User>(), res);
                return VDto<User>.OfData(Status.GET_DATA_SUCCESS, lis);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<User>.Of(Status.DATA_TO_LIST_FAIL);
            }
        }

        public VDto<User> findByOpenid(string openid) {
            var res = ud.findByOpenid(openid);
            if(res == null) {
                return VDto<User>.Of(Status.SQL_ERROR);
            }
            try {
                var usr = DBUtil.data2Model(new User(), res.Rows[0]);
                return VDto<User>.OfModel(Status.GET_DATA_SUCCESS, usr);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<User>.Of(Status.DATA_TO_MODEL_FAIL);
            }
        }

    }
}
