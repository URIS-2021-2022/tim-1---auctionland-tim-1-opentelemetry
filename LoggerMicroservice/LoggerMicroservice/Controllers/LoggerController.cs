using LoggerMicroservice.Entities;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerMicroservice.Controllers
{
    [ApiController]
    [Route("api/logger")]
    public class LoggerController : ControllerBase
    {
        private static readonly ILogger loggerManager = LogManager.GetCurrentClassLogger();

        public LoggerController()
        {
        }

        [HttpPost]
        public void CreateLog([FromBody] Entities.Logger logger)
        {
            logger.Date = DateTime.Now.Date.ToShortDateString();
            logger.Time = DateTime.Now.ToShortTimeString();
            loggerManager.Info(logger.Date + " " + logger.Time + " microservice " + logger.Service + " sent " 
                + logger.HttpMethodName.ToUpper() + " request. Response was: " +logger.Response);
        }
    }
}
