using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WX.Model;

namespace WX.Dao {
    public class UserDao {
        private Conn dao = new Conn();

        public DataTable queryAll() {
            string sql = "select openid,name,avatar,unionid from user";
            try {
                return dao.dataAdapter(sql);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public DataTable findByOpenid(string openid) {
            string sql = $"select top 1 openid,name,avatar,unionid from user where openid='{openid}'";
            try {
                return dao.dataAdapter(sql);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
