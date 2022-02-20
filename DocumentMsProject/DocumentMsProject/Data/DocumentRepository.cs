using AutoMapper;
using DocumentMsProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Data
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IMapper mapper;
        private readonly DocumentMsContext context;

        public DocumentRepository(IMapper mapper, DocumentMsContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public DocumentConfirmation CreateDocument(Document document)
        {
            var created = context.Add(document);
            return mapper.Map<DocumentConfirmation>(created.Entity);
        }

        public void DeleteDocument(Guid documentID)
        {
            context.Document.Remove(GetDocumentById(documentID));
        }

        public List<Document> GetAllDocuments()
        {
            return context.Document.ToList();
        }

        public Document GetDocumentById(Guid documentID)
        {
            return context.Document.FirstOrDefault(e => e.DocumentId.Equals(documentID));
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateDocument(Document document)
        {
            //throw new NotImplementedException();
        }
    }
}
