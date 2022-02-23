using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ComplaintMicroservice.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ComplaintMicroservice.ServiceCalls
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration configuration;

        public LoggerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool CreateLog(LoggerDto loggerDto)
        {
            using HttpClient client = new();
            var x = configuration["Services:LoggerMicroservice"];
            Uri url = new($"{ x }api/logger");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(loggerDto));
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }
    }
}
