using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly List<Document> documents = new List<Document>();

        public Document CreateDocument(Document document)
        {
            document = new Document();

            document.DocumentId = Guid.NewGuid();

            documents.Add(document);
            return document;
        }

        public void DeleteDocument(Guid documentID)
        {
            var registration = GetDocumentById(documentID);
            documents.Remove(registration);
        }

        public List<Document> GetAllDocuments()
        {
            return documents;
        }

        public Document GetDocumentById(Guid documentID)
        {
            return documents.FirstOrDefault(e => e.DocumentId == documentID);
        }

        public Document UpdateDocument(Document document)
        {
            return document;
        }
    }
}
