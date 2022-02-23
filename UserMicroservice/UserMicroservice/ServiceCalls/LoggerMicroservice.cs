using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserMicroservice.Models;

namespace UserMicroservice.ServiceCalls
{
    public class LoggerMicroservice : ILoggerMicroservice
    {
        private readonly IConfiguration configuration;

        public LoggerMicroservice(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool CreateLog(LoggerDto loggerDto)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ configuration["Services:LoggerMicroservice"] }api/logger");

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
}
