using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Dao {
    public class RecordDao {
        private Conn dao = new Conn();

        public DataTable queryAll() {
            string sql = "select id,openid,name,message,time,judge from record order by time desc";
            try {
                return dao.dataAdapter(sql);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public DataTable queryByOpenid(string openid) {
            string sql = $"select id,openid,name,message,time,judge from record where openid='{openid}' order by time desc";
            try {
                return dao.dataAdapter(sql);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public DataTable findByGuid(string guid) {
            string sql = $"select id,openid,name,message,time,judge from record where guid='{guid}' limit 1";
            try {
                return dao.dataAdapter(sql);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public int insertRecord(string id, string openid, string name, string message, string time) {
            string sql = "insert into record(id,openid,name,message,time,judge) value(@id,@openid,@name,@message,@time,0)";
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("id", id);
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

        public int updateRecord(string id, int judge) {
            string sql = "update record set judge=@judge where id=@id";
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("id", id);
            map.Add("judge", judge);
            try {
                return dao.execEdit(sql, map);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
    }
}
