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

        public async Task<PublicBiddingDto> GetPublicBiddingById(Guid? publicBiddingId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Uri url = new Uri($"{ configuration["Services:PublicBiddingService"] }api/publicBiddings/{publicBiddingId}");

                    HttpResponseMessage response = client.GetAsync(url).Result;

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var publicBidding = JsonConvert.DeserializeObject<PublicBiddingDto>(responseContent);

                    return publicBidding;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
