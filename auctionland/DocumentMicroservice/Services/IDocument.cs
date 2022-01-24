using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services
{
    public interface IDocument
    {
        ResponseDocumentDto CreateDocument(RequestDocumentDto requestDocumentDto);

        ResponseDocumentDto UpdateDocument(RequestDocumentDto responseDocumentDto);

        List<ResponseDocumentDto> GetAllDocuments();

        ResponseDocumentDto GetDocumentById(Guid documentID);

        void DeleteDocument(Guid documentID);

        //ovde ce trebati jos neki posebni 
    }
}
