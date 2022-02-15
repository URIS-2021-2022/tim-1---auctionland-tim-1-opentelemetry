using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Data.Repository
{
    public interface IListOfDocumentsRepository
    {
        ListOfDocumentsConfirmation CreateList(ListOfDocuments listOfDocumentsDto);

        void UpdateList(ListOfDocuments listOfDocumentsDto);

        List<ListOfDocuments> GetAllLists();

        ListOfDocuments GetListById(Guid listID);

        void DeleteList(Guid listID);

        bool SaveChanges();
    }
}
