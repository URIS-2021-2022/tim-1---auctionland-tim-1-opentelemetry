using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.AuthorizedPersonCustomer
{
    /// <summary>
    /// DTO za kreiranje ovlascenog lica kupca
    /// </summary>
    public class AuthorizedPersonCustomerCreationDto
    {
        /// <summary>
        /// ID ovlascenog lica kupca
        /// </summary>
        [Required]
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
