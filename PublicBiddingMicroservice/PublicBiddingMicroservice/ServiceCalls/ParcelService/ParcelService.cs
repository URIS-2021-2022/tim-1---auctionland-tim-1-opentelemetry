using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

        public bool GetParcelById(Guid parcelId)
        {
            using HttpClient client = new();
            var x = configuration["Services:ParcelService"];
            Uri url = new($"{ configuration["Services:AddressService"] }api/addresses");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(parcelId));
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
