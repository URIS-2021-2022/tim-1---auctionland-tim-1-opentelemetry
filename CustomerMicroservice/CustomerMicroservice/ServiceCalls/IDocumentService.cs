using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.ServiceCalls
{
    public interface IDocumentService
    {
        public bool GetDocumentById(Guid documentId);
    }
}
