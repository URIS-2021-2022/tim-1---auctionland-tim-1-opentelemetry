using DocumentMicroservice.Data.Repository;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services.Implementation
{
    public class ListOfDocumentsService : IListOfDocuments
    {
        private readonly IListOfDocumentsRepository listOfDocumentsRepository;

        public ListOfDocumentsService(IListOfDocumentsRepository listOfDocumentsRepository)
        {
            this.listOfDocumentsRepository = listOfDocumentsRepository;
        }

        public List<ListOfDocumentsDto> GetAllLists()
        {
            List<ListOfDocuments> lists = listOfDocumentsRepository.GetAllLists();
            List<ListOfDocumentsDto> responseListsDtos = new List<ListOfDocumentsDto>();

            foreach (ListOfDocuments list in lists)
            {
                ListOfDocumentsDto responseDto = new ListOfDocumentsDto()
                {
                    ListCreationDate = list.ListCreationDate
                };
                responseListsDtos.Add(responseDto);
            }


            return responseListsDtos;
        }

        public ListOfDocumentsDto GetListById(Guid listID)
        {
            throw new NotImplementedException();
        }

        public ListOfDocumentsDto CreateList(ListOfDocumentsDto listOfDocumentsDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteList(Guid listID)
        {
            throw new NotImplementedException();
        }

        public ListOfDocumentsDto UpdateList(ListOfDocumentsDto listOfDocumentsDto)
        {
            throw new NotImplementedException();
        }
    }
}
