using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WX.Dto;
using WX.Model;
using WX.Service;

namespace WX.Controllers {
    // 申请记录
    [Route("record")]
    [ApiController]
    public class RecordController : ControllerBase {
        private RecordService rs = new RecordImpl();

        // 获取全部
        [HttpGet("queryall")]
        public VDto<Record> findAll() {
            return rs.findAllRecord();
        }

        // 根据openid获取
        [HttpGet("querybyopenid")]
        public VDto<Record> findByOpenid(string openid) {
            if(isInvalid(openid)) {
                return VDto<Record>.Of(Status.LOST_PARAM);
            }
            return rs.findByOpenid(openid);
        }

        // 根据guid获取
        [HttpGet("getbyguid")]
        public VDto<Record> findByGuid(string guid) {
            if(isInvalid(guid)) {
                return VDto<Record>.Of(Status.LOST_PARAM);
            }
            return rs.findByGuid(guid);
        }

        private static bool isInvalid(string param) {
            return (param?.Trim()?.Length??0) == 0;
        }
    }
}