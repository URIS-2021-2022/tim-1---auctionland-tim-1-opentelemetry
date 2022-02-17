using DocumentMsProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Data
{
    public interface IListOfDocumentsRepository
    {
        ListOfDocumentsConfirmation CreateListOfDocuments(ListOfDocuments listOfDocuments);

        void UpdateListOfDocuments(ListOfDocuments listOfDocuments);

        List<ListOfDocuments> GetAllListOfDocuments();

        ListOfDocuments GetListOfDocumentsById(Guid listID);

        void DeleteListOfDocuments(Guid listID);

        bool SaveChanges();
    }
}
