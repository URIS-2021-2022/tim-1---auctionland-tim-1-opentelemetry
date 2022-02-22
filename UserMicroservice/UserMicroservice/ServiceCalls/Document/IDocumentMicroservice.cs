using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.ServiceCalls.Document
{
    public interface IDocumentMicroservice
    {
        public bool GetDocumentById(Guid documentId);
    }
}
