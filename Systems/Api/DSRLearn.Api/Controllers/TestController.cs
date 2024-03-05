﻿using System.Diagnostics;
using Asp.Versioning;
using DSRLearn.Services.Logger;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DSRLearn.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "Test")]
    [Route("v{version:apiVersion}/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IAppLogger logger;
        public TestController(IAppLogger logger)
        {
            this.logger = logger;
        }
        [HttpGet]
        [ApiVersion("1.0")]
        public int Test (int value)
        {
            logger.Debug(this, "Executed {0}, value={1}", "GET:/v1/test/", value);

            return value;
        }

        [HttpGet]
        [ApiVersion("2.0")]
        public int Test(int value1, int value2)
        {
            logger.Debug(this, "Executed {0}, value1={1}, value2={2}", "GET:/v2/test/", value1, value2);

            return value1 + value2;
        }
    }
}
