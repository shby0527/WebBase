using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Interface.Test;

namespace Controllers.Controllers
{

    [Route("api/test")]
    public class TestController : Controller
    {

        public ILogger<TestController> Logger { get; set; }

        public ITestService TestService { get; set; }

        [HttpGet]
        public string Get()
        {
            Logger.LogInformation("TestController Default Method Invorked");
            return TestService.GetAll().ToString();
        }
    }
}