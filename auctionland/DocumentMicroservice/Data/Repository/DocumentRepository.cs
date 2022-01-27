using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentContext documentContext;

        public DocumentRepository(DocumentContext documentContext)
        {
            this.documentContext = documentContext;
        }

        public Document CreateDocument(Document document)
        {
            document = new Document();

            document.DocumentId = Guid.NewGuid();


            documentContext.Documents.Add(document);
            return document;
        }

        public void DeleteDocument(Guid documentID)
        {
            var document = GetDocumentById(documentID);
            documentContext.Documents.Remove(document);
        }

        public List<Document> GetAllDocuments()
        {
            return documentContext.Documents.ToList();
        }

        public Document GetDocumentById(Guid documentID)
        {
            return documentContext.Documents.FirstOrDefault(e => e.DocumentId == documentID);
        }

        public Document UpdateDocument(Document document)
        {
            return document;
        }
    }
}
