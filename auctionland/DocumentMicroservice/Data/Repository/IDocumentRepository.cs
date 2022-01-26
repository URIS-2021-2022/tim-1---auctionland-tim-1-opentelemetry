using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services.Repository
{
    public interface IDocumentRepository
    {
        Document CreateDocument(Document document);

        Document UpdateDocument(Document document);

        List<Document> GetAllDocuments();

        Document GetDocumentById(Guid documentID);

        void DeleteDocument(Guid documentID);
    }
}
