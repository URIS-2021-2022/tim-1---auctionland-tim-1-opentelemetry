using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models;

namespace UserMicroservice.ServiceCalls.Document
{
    public interface IDocumentMicroservice
    {
        public Task<DocumentDto> GetDocument(Guid documentId);
    }
}
