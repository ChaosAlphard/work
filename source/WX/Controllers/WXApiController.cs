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

        /**
         * @param: msg 消息
         * @param: from 发送者的openid
         */
        [HttpGet("applyForRepair")]
        public VDto<SubMsgResult> applyForRepair(string msg, string from) {
            if(isInvalid(msg)&&isInvalid(from)) {
                return VDto<SubMsgResult>.Of(Status.LOST_PARAM);
            }
            // 管理者的openid
            string managerOpenid = "oqFTn5Yr2QNr_-492HBzh7I_w53Y";
            // 消息模板的templateid
            string templateid = "8yJvg3O8n7FReHC8KUDChlFMoxNIDqEAvzGr2o_Vx5o";
            // 点击跳转的page路径
            string page = $"pages/manage/manage?openid={from}&msg={msg}";
            // 通知消息的Entity
            RemindMsg message = new RemindMsg();
            message.thing1 = Val.Of(msg);
            message.date2 = Val.Of(DateUtil.getCurrentTimeStr());
            // 返回结果
            var response = wx.requireSubMsg<SubMsgResult, RemindMsg>(managerOpenid, templateid, message, page);

            return VDto<SubMsgResult>.OfModel(Status.GET_DATA_SUCCESS, response);
        }

        [HttpGet("replyRepair")]
        public VDto<User> replyRepair(string code) {
            if(isInvalid(code)) {
                return VDto<User>.Of(Status.LOST_PARAM);
            }
            throw new NotImplementedException();
        }

        private static bool isInvalid(string param) {
            return (param?.Trim()?.Length??0) == 0;
        }
    }
}