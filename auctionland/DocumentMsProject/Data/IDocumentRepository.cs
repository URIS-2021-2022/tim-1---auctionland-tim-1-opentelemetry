using DocumentMsProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Data
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
