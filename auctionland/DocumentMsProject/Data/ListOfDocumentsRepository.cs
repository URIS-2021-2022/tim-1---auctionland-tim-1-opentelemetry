using AutoMapper;
using DocumentMsProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Data
{
    public class ListOfDocumentsRepository : IListOfDocumentsRepository
    {
        private readonly IMapper mapper;
        private readonly DocumentMsContext context;

        public ListOfDocumentsRepository(IMapper mapper, DocumentMsContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public ListOfDocumentsConfirmation CreateListOfDocuments(ListOfDocuments listOfDocuments)
        {
            var created = context.Add(listOfDocuments);
            return mapper.Map<ListOfDocumentsConfirmation>(created.Entity);
        }

        public void DeleteListOfDocuments(Guid listID)
        {
            context.ListOfDocuments.Remove(GetListOfDocumentsById(listID));
        }

        public List<ListOfDocuments> GetAllListOfDocuments()
        {
            return context.ListOfDocuments.ToList();
        }

        public ListOfDocuments GetListOfDocumentsById(Guid listID)
        {
            return context.ListOfDocuments.FirstOrDefault(e => e.ListOfDocumentsId.Equals(listID));
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateListOfDocuments(ListOfDocuments listOfDocuments)
        {
            //throw new NotImplementedException();
        }
    }
}
