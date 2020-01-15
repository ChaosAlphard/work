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
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase {
        private UserService us = new UserImpl();

        [HttpGet("getsession")]
        public VDto<UserSession> getSession(string code) {
            if(isInvalid(code)) {
                return VDto<UserSession>.Of(Status.LOST_PARAM);
            }

            return us.getSession(code);
        }

        [HttpPost("updateinfo")]
        public VDto<User> updateUserInfo(User usr) {
            string openid = usr?.openid??"";
            string name = usr?.name??"";
            if(isInvalid(openid)||isInvalid(name)) {
                return VDto<User>.Of(Status.LOST_PARAM);
            }
            string avatarUrl = isInvalid(usr.avatar)?usr.avatar:"";

            return us.updateUser(openid, name, avatarUrl);
        }

        [HttpGet("findlevel")]
        public VDto<Int32> findLevel(string openid) {
            if(isInvalid(openid)) {
                return VDto<Int32>.Of(Status.LOST_PARAM);
            }
            return us.findLevelByOpenid(openid);
        }

        private static bool isInvalid(string param) {
            return (param?.Trim()?.Length??0) == 0;
        }
    }
}