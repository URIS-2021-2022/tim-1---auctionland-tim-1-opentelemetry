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

        public ResponseDocumentDto CreateDocument(RequestDocumentDto requestDocumentDto)
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

            ResponseDocumentDto response = new ResponseDocumentDto()
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

        public List<ResponseDocumentDto> GetAllDocuments()
        {
            List<Document> documents = _documentRepository.GetAllDocuments();
            List<ResponseDocumentDto> responseDocumentDtos = new List<ResponseDocumentDto>();

            foreach (Document document in documents)
            {
                ResponseDocumentDto responseDto = new ResponseDocumentDto()
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
        
        public ResponseDocumentDto GetDocumentById(Guid documentID)
        {
            Document document = _documentRepository.GetDocumentById(documentID);

            ResponseDocumentDto response = new ResponseDocumentDto()
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

        public ResponseDocumentDto UpdateDocument(RequestDocumentDto requestDocumentDto)
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

            ResponseDocumentDto response = new ResponseDocumentDto()
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
