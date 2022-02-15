using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services
{
    public interface IListOfDocuments
    {
        ListOfDocumentsDto CreateList(ListOfDocumentsDto listOfDocumentsDto);

        ListOfDocumentsDto UpdateList(ListOfDocumentsDto listOfDocumentsDto);

        List<ListOfDocumentsDto> GetAllLists();

        ListOfDocumentsDto GetListById(Guid listID);

        void DeleteList(Guid listID);
    }
}
