﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PublicBiddingRegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.ServiceCalls.CustomerMicroservice
{
    public class CustomerMicroservice : ICustomerMicroservice
    {
        private readonly IConfiguration configuration;

        public CustomerMicroservice(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(Guid? customerId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Uri url = new Uri($"{ configuration["Services:CustomerMicroservice"] }api/customers/{customerId}");

                    HttpResponseMessage response = client.GetAsync(url).Result;

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<CustomerDto>(responseContent);

                    return customer;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
