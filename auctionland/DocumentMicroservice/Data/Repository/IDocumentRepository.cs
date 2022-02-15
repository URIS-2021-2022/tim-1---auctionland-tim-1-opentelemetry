using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services.Repository
{
    public interface IDocumentRepository
    {
        DocumentConfirmation CreateDocument(Document document);

        void UpdateDocument(Document document);

        List<Document> GetAllDocuments();

        Document GetDocumentById(Guid documentID);

        void DeleteDocument(Guid documentID);

        bool SaveChanges();
    }
}
