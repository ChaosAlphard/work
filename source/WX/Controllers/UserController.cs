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
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase {
        private UserService us = new UserImpl();

        [HttpGet("info.get")]
        public VDto<User> getUserInfo(string code) {
            if(isInvalid(code)) {
                return VDto<User>.Of(Status.LOST_PARAM);
            }
            Console.WriteLine(code);
            return us.findAll();
        }

        private static bool isInvalid(string param) {
            return (param?.Trim()?.Length??0) == 0;
        }
    }
}