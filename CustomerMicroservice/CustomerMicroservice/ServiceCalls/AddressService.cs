﻿using CustomerMicroservice.Models;
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

        public async Task<AddressDto> GetAddress(Guid addressId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:AddressService"] }api/addresses/" + addressId);
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
                    return JsonConvert.DeserializeObject<AddressDto>(content);
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
