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
            logger.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            logger.Time = DateTime.Now.ToString("hh:mm tt");
            string message = logger.Date + " " + logger.Time + " Microservice " + logger.Service.ToUpper() + " sent "
                + logger.HttpMethodName.ToUpper() + " request. Response was: " + logger.Response.ToUpper();
            if (logger.Response.StartsWith("2"))
            {
                loggerManager.Info(message);
            }
            else if (logger.Response.StartsWith("4"))
            {
                loggerManager.Warn(message);
            }
            else
            {
                loggerManager.Error(message);
            }
            
        }
    }
}
