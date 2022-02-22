using DocumentMsProject.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DocumentMsProject.ServiceCalls.PublicBiddingMicroservice
{
    public class PublicBiddingMicroservice : IPublicBiddingMicroservice
    {
        private readonly IConfiguration configuration;

        public PublicBiddingMicroservice(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool GetPublicBiddings(PublicBiddingDto publicBiddingDto)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Uri url = new Uri($"{ configuration["Services:PublicBiddingMicroservice"] }api/publicBiddings");

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(publicBiddingDto));
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
