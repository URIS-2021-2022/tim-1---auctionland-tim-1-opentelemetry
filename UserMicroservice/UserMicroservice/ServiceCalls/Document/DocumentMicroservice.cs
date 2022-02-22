using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UserMicroservice.ServiceCalls.Document
{
    public class DocumentMicroservice : IDocumentMicroservice
    {
        private readonly IConfiguration configuration;

        public DocumentMicroservice(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool GetDocumentById(Guid documentId)
        {
            using HttpClient client = new();
            var x = configuration["Services:DocumentMicrosevice"];
            Uri url = new($"{ configuration["Services:AddressService"] }api/addresses");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(documentId));
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
