using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Dao {
    public class Conn {
        private static string conf = "Database=wx;Data Source=127.0.0.1;port=3306;" +
            "User Id=root;Password=123456;CharSet=UTF8;pooling=false";

        private MySqlConnection getConn() {
            return new MySqlConnection(conf);
        }

        public DataTable dataAdapter(string sql) {
            using(var conn = getConn()) {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                var res = new MySqlDataAdapter(cmd);
                DataTable data = new DataTable();
                res.Fill(data);
                return data;
            }
        }

        public object execScalar(string sql) {
            using(var conn = getConn()) {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                return cmd.ExecuteScalar();
            }
        }

        public MySqlDataReader execReader(string sql) {
            using(var conn = getConn()) {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                return cmd.ExecuteReader();
            }
        }

        public int execEdit(string sql) {
            using(var conn = getConn()) {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
