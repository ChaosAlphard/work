using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Dao {
    public class RecordDao {
        private Conn dao = new Conn();

        public DataTable queryAll() {
            string sql = "select id,openid,name,message,time,judge from record";
            try {
                return dao.dataAdapter(sql);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public DataTable queryByOpenid(string openid) {
            string sql = $"select id,openid,name,message,time,judge from record where openid='{openid}'";
            try {
                return dao.dataAdapter(sql);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public int insertRecord(string openid, string name, string message, DateTimeOffset time) {
            string sql = "insert into record(openid,name,message,time,judge) value(@openid,@name,@message,@time,0)";
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("openid", openid);
            map.Add("name", name);
            map.Add("message", message);
            map.Add("time", time);
            try {
                return dao.execEdit(sql, map);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
    }
}
