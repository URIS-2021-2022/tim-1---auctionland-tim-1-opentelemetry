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
    public class PublicBiddingService : IPublicBiddingService
    {
        private readonly IConfiguration configuration;

        public PublicBiddingService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool GetPublicBidding(PublicBiddingDto publicBiddingDto)
        {
            using HttpClient client = new();
            var x = configuration["Services:PublicBiddingService"];
            Uri url = new($"{ configuration["Services:PublicBiddingService"] }api/publicBiddings");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(publicBiddingDto));
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
