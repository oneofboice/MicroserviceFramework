using Api.Interfaces;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Controllers
{
    public class LogController : ApiController
    {
        private readonly ILoggerFunctions _loggerFunctions;

        public LogController(ILoggerFunctions loggerFunctions)
        {
            _loggerFunctions = loggerFunctions;
        }

        [HttpPost]
        [Route("Report")]
        public bool Report([FromBody] LogMessage message)
        {
            try
            {
                _loggerFunctions.Report(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
