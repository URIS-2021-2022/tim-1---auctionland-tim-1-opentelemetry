using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using DocumentMicroservice.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services.Implementation
{
    public class DocumentService : IDocument
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public DocumentCreationDto CreateDocument(DocumentDto requestDocumentDto)
        {
            Document document = new Document(){
                DocumentSerialNumber = requestDocumentDto.DocumentSerialNumber,
                DocumentName = requestDocumentDto.DocumentName,
                DocumentDate = requestDocumentDto.DocumentDate,
                DocumentSubmissionDate = requestDocumentDto.DocumentSubmissionDate,
                DocumentTemplate = requestDocumentDto.DocumentTemplate,
                ListOfDocuments = requestDocumentDto.ListOfDocuments
            };

            _documentRepository.CreateDocument(document);

            DocumentCreationDto response = new DocumentCreationDto()
            {
                DocumentSerialNumber = document.DocumentSerialNumber,
                DocumentName = document.DocumentName,
                DocumentDate = document.DocumentDate,
                DocumentSubmissionDate = document.DocumentSubmissionDate,
                DocumentTemplate = document.DocumentTemplate,
                ListOfDocuments = document.ListOfDocuments
            };

            return response;
        }

        public void DeleteDocument(Guid documentID)
        {
            _documentRepository.DeleteDocument(documentID);
        }

        public List<DocumentCreationDto> GetAllDocuments()
        {
            List<Document> documents = _documentRepository.GetAllDocuments();
            List<DocumentCreationDto> responseDocumentDtos = new List<DocumentCreationDto>();

            foreach (Document document in documents)
            {
                DocumentCreationDto responseDto = new DocumentCreationDto()
                {
                    DocumentSerialNumber = document.DocumentSerialNumber,
                    DocumentName = document.DocumentName,
                    DocumentDate = document.DocumentDate,
                    DocumentSubmissionDate = document.DocumentSubmissionDate,
                    DocumentTemplate = document.DocumentTemplate,
                    ListOfDocuments = document.ListOfDocuments
                };
                responseDocumentDtos.Add(responseDto);
            }


            return responseDocumentDtos;
        }
        
        public DocumentCreationDto GetDocumentById(Guid documentID)
        {
            Document document = _documentRepository.GetDocumentById(documentID);

            DocumentCreationDto response = new DocumentCreationDto()
            {
                DocumentSerialNumber = document.DocumentSerialNumber,
                DocumentName = document.DocumentName,
                DocumentDate = document.DocumentDate,
                DocumentSubmissionDate = document.DocumentSubmissionDate,
                DocumentTemplate = document.DocumentTemplate,
                ListOfDocuments = document.ListOfDocuments
            };

            return response;
        }

        public DocumentCreationDto UpdateDocument(DocumentDto requestDocumentDto)
        {
            Document document = new Document()
            {
                DocumentSerialNumber = requestDocumentDto.DocumentSerialNumber,
                DocumentName = requestDocumentDto.DocumentName,
                DocumentDate = requestDocumentDto.DocumentDate,
                DocumentSubmissionDate = requestDocumentDto.DocumentSubmissionDate,
                DocumentTemplate = requestDocumentDto.DocumentTemplate,
                ListOfDocuments = requestDocumentDto.ListOfDocuments
            };

            _documentRepository.UpdateDocument(document);

            DocumentCreationDto response = new DocumentCreationDto()
            {
                DocumentSerialNumber = document.DocumentSerialNumber,
                DocumentName = document.DocumentName,
                DocumentDate = document.DocumentDate,
                DocumentSubmissionDate = document.DocumentSubmissionDate,
                DocumentTemplate = document.DocumentTemplate,
                ListOfDocuments = document.ListOfDocuments
            };

            return response;
        }
    }
}
