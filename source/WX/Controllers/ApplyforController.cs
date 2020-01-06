using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WX.Model;
using WX.Util;

namespace WX.Controllers {
    [ApiController]
    [Route("applyfor")]
    public class ApplyforController : ControllerBase {
        [HttpGet]
        public string Index() {
            return "Hello World!";
        }
    }
}
