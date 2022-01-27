using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services
{
    public interface IListOfDocuments
    {
        ResponseListOfDocumentsDto CreateList(RequestListOfDocumentsDto listOfDocumentsDto);

        ResponseListOfDocumentsDto UpdateList(RequestListOfDocumentsDto listOfDocumentsDto);

        List<ResponseListOfDocumentsDto> GetAllLists();

        ResponseListOfDocumentsDto GetListById(Guid listID);

        void DeleteList(Guid listID);
    }
}
