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

        [HttpGet("getinfo")]
        public VDto<string> getUserInfo(string code) {
            if(isInvalid(code)) {
                return VDto<string>.Of(Status.LOST_PARAM);
            }

            return us.getUserInfo(code);
        }

        [HttpGet("updateinfo")]
        public VDto<User> updateUserInfo(string openid, string name, string avatar) {
            if(isInvalid(openid)&&isInvalid(name)&&isInvalid(avatar)) {
                return VDto<User>.Of(Status.LOST_PARAM);
            }

            return us.updateUser(openid, name, avatar);
        }

        private static bool isInvalid(string param) {
            return (param?.Trim()?.Length??0) == 0;
        }
    }
}