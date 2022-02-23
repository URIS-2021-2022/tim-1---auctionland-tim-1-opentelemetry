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
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration configuration;

        public CustomerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<CustomerDto> GetCustomerById(Guid customerId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new($"{ configuration["Services:CustomerService"] }api/customers/" + customerId);
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
                    return JsonConvert.DeserializeObject<CustomerDto>(content);
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
