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
            string sql = "select openid,name,avatar,unionid,level from user";
            try {
                return dao.dataAdapter(sql);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public DataTable findByOpenid(string openid) {
            string sql = $"select openid,name,avatar,unionid,level from user where openid='{openid}' limit 1";
            try {
                return dao.dataAdapter(sql);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public DataTable findLevelByOpenid(string openid) {
            string sql = $"select level from user where openid='{openid}' limit 1";
            try {
                return dao.dataAdapter(sql);
            } catch(Exception ex) {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public int insertUser(string openid, string name, string avatar) {
            string sql = "insert into user(name, openid, avatar, level) value(@name, @openid, @avatar, 1)";
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("name", name);
            map.Add("openid", openid);
            map.Add("avatar", avatar);
            try {
                return dao.execEdit(sql, map);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public int updateUser(string openid, string name, string avatar) {
            string sql = "update user set name=@name, avatar=@avatar where openid=@openid";
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("name", name);
            map.Add("openid", openid);
            map.Add("avatar", avatar);
            try {
                return dao.execEdit(sql, map);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
    }
}
