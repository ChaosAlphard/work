using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WX.Cache;
using WX.Dto;
using WX.Model;
using WX.Service;
using WX.Util;

namespace WX.Controllers {
    [ApiController]
    [Route("wx")]
    public class WXApiController : ControllerBase {
        private WXApiService wx = new WXApiImpl();
        private RecordService rs = new RecordImpl();

        private static int AGREE = 1;
        private static int DISAGREE = 2;

        /**
         * @param: msg 消息
         * @param: from 发送者的openid
         */
        [HttpPost("applyForRepair")]
        public VDto<SubMsgResult> applyForRepair(Record record) {
            // 申请者的openid
            string openid = record?.openid??"";
            // 申请者的名称
            string name = record?.name??"";
            // 申请信息
            string message = record?.message??"";
            if(isInvalid(openid)||isInvalid(name)||isInvalid(message)) {
                return VDto<SubMsgResult>.Of(Status.LOST_PARAM);
            }
            // 申请Entity的GUID(作主键)
            string guid = RandomUtil.getGUID();
            record.id = guid;
            record.time = DateUtil.getCurrentTimeStr();

            // 插入申请记录
            int i = rs.insertRecord(record);
            if(i < 0) {
                return VDto<SubMsgResult>.Of(Status.SQL_ERROR);
            } else if(i == 0) {
                return VDto<SubMsgResult>.Of(Status.INSERT_FAIL);
            }

            // 插入成功后下发通知
            // 管理者的openid
            string managerOpenid = "oqFTn5Yr2QNr_-492HBzh7I_w53Y";
            // 消息模板的templateid, 前端申请订阅的templateid要和这个一致
            string templateid = "AHpAmF18906B5wZ_zsB799T8KHi4wPtK4pL0Ro6a4Ew";
            // 点击跳转的page路径
            string page = $"pages/manage/manage?id={guid}";
            // 通知消息的Entity
            // 微信要求格式为
            // {
            //   name1: {
            //     value: "名字"
            //   }, 
            //   thing2: {
            //     value: "消息"
            //   }
            // }
            // 故用Val.Of嵌套一层
            RemindMsg msg = new RemindMsg();
            msg.name1 = Val.Of(name);
            msg.thing2 = Val.Of(message);
            msg.time3 = Val.Of(DateUtil.getCurrentTimeStr());
            
            try {
                // 返回结果
                var response = wx.requireSubMsg<SubMsgResult, RemindMsg>(managerOpenid, templateid, msg, page);
                return VDto<SubMsgResult>.OfModel(Status.GET_DATA_SUCCESS, response);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<SubMsgResult>.Of(Status.GET_DATA_FAIL);
            }
        }

        [HttpPost("replyRepair")]
        public VDto<SubMsgResult> replyRepair(JudgeEntity je) {
            if(!je.isValid()) {
                return VDto<SubMsgResult>.Of(Status.LOST_PARAM);
            }
            // 是否同意
            bool flag = "true".Equals(je.isAgree);
            // 更新数据库
            int i = rs.updateRecord(je.id, flag ? AGREE : DISAGREE);
            if(i < 0) {
                return VDto<SubMsgResult>.Of(Status.SQL_ERROR);
            } else if(i == 0) {
                return VDto<SubMsgResult>.Of(Status.UPDATE_FAIL);
            }

            // 更新成功后下发通知
            string templateid = "AHpAmF18906B5wZ_zsB799T8KHi4wPtK4pL0Ro6a4Ew";
            RemindMsg message = new RemindMsg();
            message.name1 = Val.Of("Alphard");
            message.thing2 = Val.Of(flag?"通过":"不通过");
            message.time3 = Val.Of(DateUtil.getCurrentTimeStr());
            try {
                var response = wx.requireSubMsg<SubMsgResult, RemindMsg>(je.openid, templateid, message, null);
                return VDto<SubMsgResult>.OfModel(Status.GET_DATA_SUCCESS, response);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<SubMsgResult>.Of(Status.GET_DATA_FAIL);
            }
        }

        private static bool isInvalid(string param) {
            return (param?.Trim()?.Length??0) == 0;
        }
    }
}