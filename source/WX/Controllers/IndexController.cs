using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WX.Controllers {
    [ApiController]
    [Route("/")]
    public class IndexController : ControllerBase {
        [HttpGet]
        public string Index() {
            return "Hello World!";
        }
    }
}
