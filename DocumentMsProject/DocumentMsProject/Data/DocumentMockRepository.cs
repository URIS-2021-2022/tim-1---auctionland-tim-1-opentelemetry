using DocumentMsProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Data
{
    public class DocumentMockRepository : IDocumentRepository
    {
        public static List<Document> Documents { get; set; } = new List<Document>();

        public DocumentMockRepository()
        {
            FillData();
        }

        public void FillData()
        {
            Documents.AddRange(new List<Document>
            {
                new Document
                {
                    DocumentId = Guid.Parse("8b44e760-3c55-4ae4-8d1b-c4ea053672ff"),
                    DocumentSerialNumber = 1,
                    DocumentName = "First",
                    DocumentDate = DateTime.Parse("2008-12-20T10:00:00"),
                    DocumentSubmissionDate = DateTime.Parse("2021-12-20T10:00:00"),
                    DocumentTemplate = "FTemplate",
                    ListOfDocumentsID = Guid.Parse("9791d7f8-ee0e-4426-9bdb-2600d8aa8975")
                }
            });
        }

        public DocumentConfirmation CreateDocument(Document document)
        {
            document.DocumentId = Guid.NewGuid();
            Documents.Add(document);
            Document documentAdd = GetDocumentById(document.DocumentId);

            return new DocumentConfirmation
            {
                DocumentId = documentAdd.DocumentId,
                DocumentSerialNumber = documentAdd.DocumentSerialNumber,
                DocumentName = documentAdd.DocumentName,
                DocumentDate = documentAdd.DocumentDate,
                DocumentSubmissionDate = documentAdd.DocumentSubmissionDate,
                ListOfDocumentsID = documentAdd.ListOfDocumentsID

            };
        }

        public void DeleteDocument(Guid documentID)
        {
            Documents.Remove(Documents.FirstOrDefault(e => e.DocumentId == documentID));
        }

        public List<Document> GetAllDocuments()
        {
            return (from e in Documents
                    select e).ToList();
        }

        public Document GetDocumentById(Guid documentID)
        {
            return Documents.FirstOrDefault(e => e.DocumentId == documentID);
        }

        public bool SaveChanges()
        {
            return true;
        }

        public void UpdateDocument(Document document)
        {
            var documentvar = GetDocumentById(document.DocumentId);

            documentvar.DocumentId = document.DocumentId;
            documentvar.DocumentSerialNumber = document.DocumentSerialNumber;
            documentvar.DocumentName = document.DocumentName;
            documentvar.DocumentDate = document.DocumentDate;
            documentvar.DocumentSubmissionDate = document.DocumentSubmissionDate;
            documentvar.ListOfDocumentsID = document.ListOfDocumentsID;
        }
    }
}
