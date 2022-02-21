using Microsoft.Extensions.Configuration;
using PublicBiddingMicroservice.Models;
using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace PublicBiddingMicroservice.ServiceCalls
{
    public class AddressService : IAddressService
    {
        private readonly IConfiguration configuration;

        public AddressService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool GetAddressOfPubblicBidding(AddressDto addressDto)
        {
            using HttpClient client = new();
            var x = configuration["Services:AddressService"];
            Uri url = new($"{ configuration["Services:AddressService"] }api/addresses");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(addressDto));
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
