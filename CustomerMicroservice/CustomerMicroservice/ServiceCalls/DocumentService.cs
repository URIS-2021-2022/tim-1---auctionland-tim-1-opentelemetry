using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerMicroservice.ServiceCalls
{
    public class DocumentService : IDocumentService
    {
        private readonly IConfiguration configuration;

        public DocumentService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool GetDocumentById(Guid documentId)
        {
            using HttpClient client = new();
            var x = configuration["Services:DocumentService"];
            Uri url = new($"{ configuration["Services:DocumentService"] }api/document");

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
