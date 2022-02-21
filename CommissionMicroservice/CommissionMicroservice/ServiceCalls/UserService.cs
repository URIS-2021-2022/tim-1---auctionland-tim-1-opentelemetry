using CommissionMicroservice.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommissionMicroservice.ServiceCalls
{
    public class UserService : IUserService
    {
        private readonly IConfiguration configuration;

        public UserService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool GetUserOfCommission(UserDto userDto)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:UserService"];
                Uri url = new Uri($"{ configuration["Services:UserService"] }api/users");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(userDto));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
