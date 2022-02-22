using DocumentMsProject.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DocumentMsProject.ServiceCalls.CommissionMicroservice
{
    public class CommissionMicroservice : ICommissionMicroservice
    {
        private readonly IConfiguration configuration;

        public CommissionMicroservice(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool GetMembers(CommissionDto commissionDto)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Uri url = new Uri($"{ configuration["Services:CommissionMicroservice"] }api/members");

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(commissionDto));
                    content.Headers.ContentType.MediaType = "application/json";

                    HttpResponseMessage response = client.PostAsync(url, content).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
