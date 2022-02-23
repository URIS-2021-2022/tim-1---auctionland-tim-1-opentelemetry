using LoggerMicroservice.Entities;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Metoda služi za ručno upisivanje logova. Koristiti samo u izuzetnim situacijama.
        /// </summary>
        /// <param name="logger">Model koji prikazuje strukturu jednog upisa u log fajl.</param>
        /// <returns></returns>
        /// <remarks>
        /// Primer jednog upisa u log fajl. \
        /// POST /api/logger \
        /// {     \
        ///     "date": "Wednesday, 23 February 2022", \
        ///     "time": "12:15 AM", \
        ///     "service": "ADDRESS", \
        ///     "methodName": "GET", \
        ///     "response": "200 OK" \
        ///}
        /// </remarks>
        /// <response code="200">Uspešno upisan log.</response>
        /// <response code="500">Došlo je do greške na serveru prilikom upisivanja log-a.</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateLog([FromBody] Entities.Logger logger)
        {
            try
            {
                logger.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                logger.Time = DateTime.Now.ToString("hh:mm tt");
                string message = logger.Date + " " + logger.Time + " Microservice " + logger.Service.ToUpper() + " sent "
                    + logger.HttpMethodName.ToUpper() + " request. Response was: " + logger.Response.ToUpper();

                if (logger.Response.StartsWith("2"))
                {
                    loggerManager.Info(message);
                    return Ok(message);
                }
                else if (logger.Response.StartsWith("4"))
                {
                    loggerManager.Warn(message);
                    return Ok(message);
                }
                else
                {
                    loggerManager.Error(message);
                    return Ok(message);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Log error");
            }
        }
    }
}
