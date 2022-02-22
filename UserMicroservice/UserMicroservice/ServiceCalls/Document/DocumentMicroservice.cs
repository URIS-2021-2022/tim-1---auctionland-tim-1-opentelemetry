using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserMicroservice.Models;

namespace UserMicroservice.ServiceCalls.Document
{
    public class DocumentMicroservice : IDocumentMicroservice
    {
        private readonly IConfiguration configuration;

        public DocumentMicroservice(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<DocumentDto> GetDocument(Guid documentId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:DocumentMicroService"] }api/documents/" + documentId);
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
                    return JsonConvert.DeserializeObject<DocumentDto>(content);
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
