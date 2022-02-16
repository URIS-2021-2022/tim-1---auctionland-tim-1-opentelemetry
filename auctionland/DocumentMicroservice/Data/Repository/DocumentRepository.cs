using AutoMapper;
using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentDbContext documentContext;
        private readonly IMapper mapper;

        public DocumentRepository(DocumentDbContext documentContext, IMapper mapper)
        {
            this.documentContext = documentContext;
            this.mapper = mapper;
        }

        public DocumentConfirmation CreateDocument(Document document)
        {
            var createdEntity = documentContext.Add(document);
            return mapper.Map<DocumentConfirmation>(createdEntity.Entity);
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
            return documentContext.Documents.FirstOrDefault(e => e.DocumentId.Equals(documentID));
        }

        public bool SaveChanges()
        {
            return documentContext.SaveChanges() > 0;
        }

        public void UpdateDocument(Document document)
        {
            //nista
        }
    }
}
