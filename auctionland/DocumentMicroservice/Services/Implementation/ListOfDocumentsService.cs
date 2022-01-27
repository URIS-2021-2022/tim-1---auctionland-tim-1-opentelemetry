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

        public List<ResponseListOfDocumentsDto> GetAllLists()
        {
            List<ListOfDocuments> lists = listOfDocumentsRepository.GetAllLists();
            List<ResponseListOfDocumentsDto> responseListsDtos = new List<ResponseListOfDocumentsDto>();

            foreach (ListOfDocuments list in lists)
            {
                ResponseListOfDocumentsDto responseDto = new ResponseListOfDocumentsDto()
                {
                    ListCreationDate = list.ListCreationDate
                };
                responseListsDtos.Add(responseDto);
            }


            return responseListsDtos;
        }

        public ResponseListOfDocumentsDto GetListById(Guid listID)
        {
            throw new NotImplementedException();
        }

        public ResponseListOfDocumentsDto CreateList(RequestListOfDocumentsDto listOfDocumentsDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteList(Guid listID)
        {
            throw new NotImplementedException();
        }

        public ResponseListOfDocumentsDto UpdateList(RequestListOfDocumentsDto listOfDocumentsDto)
        {
            throw new NotImplementedException();
        }
    }
}
