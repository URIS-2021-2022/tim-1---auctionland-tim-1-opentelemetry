using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Entities
{
    public class AuthorizedPersonCustomerConfirmation
    {
        /// <summary>
        /// ID ovlascenog lica kupca
        /// </summary>
        [Key]
        public Guid AuthorizedPersonCustomerID { get; set; }

        /// <summary>
        /// Strani kljuc ovlascenog lica
        /// </summary>
        [ForeignKey("AuthorizedPerson")]
        public Guid AuthorizedPersonID { get; set; }

        /// <summary>
        /// Strani kljuc kupca
        /// </summary>
        [ForeignKey("Customer")]
        public Guid CustomerID { get; set; }
    }
}
