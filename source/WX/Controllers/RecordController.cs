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
    [Route("record")]
    [ApiController]
    public class RecordController : ControllerBase {
        private RecordService rs = new RecordImpl();

        [HttpGet("queryall")]
        public VDto<Record> findAll() {
            return rs.findAllRecord();
        }

        [HttpGet("querybyopenid")]
        public VDto<Record> findByOpenid(string openid) {
            if(isInvalid(openid)) {
                return VDto<Record>.Of(Status.LOST_PARAM);
            }
            return rs.findByOpenid(openid);
        }

        private static bool isInvalid(string param) {
            return (param?.Trim()?.Length??0) == 0;
        }
    }
}