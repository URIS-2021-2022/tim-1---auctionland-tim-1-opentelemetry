using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PublicBiddingRegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.ServiceCalls
{
    public class PublicBiddingService : IPublicBiddingService
    {
        private readonly IConfiguration configuration;

        public PublicBiddingService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool GetPublicBidding(Guid publicBiddingId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:PublicBiddingService"];
                Uri url = new Uri($"{ configuration["Services:PublicBiddingService"] }api/publicBiddings");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(publicBiddingId));
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
