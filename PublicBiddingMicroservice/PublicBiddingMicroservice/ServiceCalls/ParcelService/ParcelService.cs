using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PublicBiddingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.ServiceCalls.ParcelService
{
    public class ParcelService : IParcelService
    {
        private readonly IConfiguration configuration;

        public ParcelService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<ParcelDto> GetParcel(Guid parcelId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:ParcelService"] }api/parcels/" + parcelId);
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
                    return JsonConvert.DeserializeObject<ParcelDto>(content);
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
