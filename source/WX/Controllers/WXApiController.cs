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

namespace WX.Controllers {
    [ApiController]
    [Route("wx")]
    public class WXApiController : ControllerBase {
        private AccessTokenCache cache = new AccessTokenCache();

        [HttpGet("applyForRepair")]
        public VDto<User> applyForRepair(string code) {
            if(isInvalid(code)) {
                return VDto<User>.Of(Status.LOST_PARAM);
            }
            throw new NotImplementedException();
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