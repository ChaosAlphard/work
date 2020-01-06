using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Dto {
    public static class Status {
        // 通用
        public static readonly
            ValueTuple<int, string> GET_DATA_SUCCESS = ValueTuple.Create(200, "OK");
        public static readonly
            ValueTuple<int, string> GET_DATA_FAIL = ValueTuple.Create(500, "获取数据失败");
        public static readonly
            ValueTuple<int, string> LOST_PARAM = ValueTuple.Create(400, "参数丢失");
        public static readonly
            ValueTuple<int, string> NOT_FOUND = ValueTuple.Create(404, "找不到对应的数据");

        // 订阅消息
        public static readonly
            ValueTuple<int, string> SEND_SUB_MSG_SUCCESS = ValueTuple.Create(200, "发送成功");
        public static readonly
            ValueTuple<int, string> SEND_SUB_MSG_FAIL = ValueTuple.Create(510, "发送失败");

        // SQL
        public static readonly 
            ValueTuple<int, string> INSERT_SUCCESS = ValueTuple.Create(200, "添加成功");
        public static readonly 
            ValueTuple<int, string> UPDATE_SUCCESS = ValueTuple.Create(200, "修改成功");
        public static readonly 
            ValueTuple<int, string> DELETE_SUCCESS = ValueTuple.Create(200, "删除成功");
        public static readonly 
            ValueTuple<int, string> INSERT_FAIL = ValueTuple.Create(501, "添加失败");
        public static readonly 
            ValueTuple<int, string> UPDATE_FAIL = ValueTuple.Create(502, "修改失败");
        public static readonly 
            ValueTuple<int, string> DELETE_FAIL = ValueTuple.Create(503, "删除失败");
        public static readonly 
            ValueTuple<int, string> SQL_ERROR = ValueTuple.Create(504, "SQL执行异常");

        // 数据转换
        public static readonly
            ValueTuple<int, string> DATA_TO_MODEL_FAIL = ValueTuple.Create(505, "数据结果集转换到实体类失败");
        public static readonly
            ValueTuple<int, string> DATA_TO_LIST_FAIL = ValueTuple.Create(505, "数据结果集转换到实体列表失败");
    }
}
