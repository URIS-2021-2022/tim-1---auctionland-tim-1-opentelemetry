using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerMicroservice.ServiceCalls
{
    public class AddressService : IAddressService
    {
        private readonly IConfiguration configuration;

        public AddressService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool GetAddressById(Guid addressID)
        {
            using HttpClient client = new();
            var x = configuration["Services:AddressService"];
            Uri url = new($"{ configuration["Services:AddressService"] }api/addresses");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(addressID));
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
