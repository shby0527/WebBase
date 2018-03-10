using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Controllers.Controllers
{

    [Route("api/test")]
    public class TestController : Controller
    {

        public ILogger<TestController> Logger { get; set; }

        [HttpGet]
        public string Get()
        {
            Logger.LogInformation("TestController Default Method Invorked");
            return "Test";
        }
    }
}