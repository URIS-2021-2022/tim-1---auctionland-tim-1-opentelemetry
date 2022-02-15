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
        DocumentCreationDto CreateDocument(DocumentDto requestDocumentDto);

        DocumentCreationDto UpdateDocument(DocumentDto responseDocumentDto);

        List<DocumentCreationDto> GetAllDocuments();

        DocumentCreationDto GetDocumentById(Guid documentID);

        void DeleteDocument(Guid documentID);

        //ovde ce trebati jos neki posebni 
    }
}
