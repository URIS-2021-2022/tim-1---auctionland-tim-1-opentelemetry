using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    public class CustomerDto
    {
        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid CustomerID { get; set; }

        /// <summary>
        /// Da li je kupac fizicko lice, ako nije onda je pravno
        /// </summary>
        public bool IsPhysicalPerson { get; set; }

        /// <summary>
        /// Prioritet 
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Ostvarena povrsina
        /// </summary>
        public string RealizedArea { get; set; }

        /// <summary>
        /// Da li kupac ima zabranu
        /// </summary>
        public bool HasABan { get; set; }  
    }
}
