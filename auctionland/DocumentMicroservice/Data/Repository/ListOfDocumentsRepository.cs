using AutoMapper;
using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Data.Repository
{
    public class ListOfDocumentsRepository : IListOfDocumentsRepository
    {
        private readonly DocumentContext context;
        private readonly IMapper mapper;

        public ListOfDocumentsRepository(DocumentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<ListOfDocuments> GetAllLists()
        {
            return context.ListOfDocuments.ToList();
        }

        public ListOfDocuments GetListById(Guid listID)
        {
            throw new NotImplementedException();
        }

        public ListOfDocuments CreateList(ListOfDocuments listOfDocumentsDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteList(Guid listID)
        {
            throw new NotImplementedException();
        }

        public ListOfDocuments UpdateList(ListOfDocuments listOfDocumentsDto)
        {
            throw new NotImplementedException();
        }
    }
}
