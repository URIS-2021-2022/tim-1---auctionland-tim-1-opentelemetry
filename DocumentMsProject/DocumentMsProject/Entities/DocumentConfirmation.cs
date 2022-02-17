using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Entities
{
    public class DocumentConfirmation
    {
        public Guid DocumentId { get; set; }

        public int DocumentSerialNumber { get; set; }

        public string DocumentName { get; set; }

        public DateTime DocumentDate { get; set; }

        public DateTime DocumentSubmissionDate { get; set; }

        #region ListOfDocuments
        public Guid ListOfDocumentsID { get; set; }
        #endregion
    }
}
