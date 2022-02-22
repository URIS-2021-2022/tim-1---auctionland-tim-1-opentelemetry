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

        public async Task<PublicBiddingDto> GetPublicBiddingById(Guid publicBiddingId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new($"{ configuration["Services:PublicBiddingService"] }api/publicBiddings/" + publicBiddingId);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default;
                    }
                    return JsonConvert.DeserializeObject<PublicBiddingDto>(content);
                }
                return default;
            }
            catch
            {
                return default;
            }
        }
    }
}
