using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.AuthorizedPersonCustomer
{
    /// <summary>
    /// DTO za potvrdjivanje ovlascenog lica kupca
    /// </summary>
    public class AuthorizedPersonCustomerConfirmationDto
    {
        /// <summary>
        /// ID ovlascenog lica kupca
        /// </summary>
        public Guid AuthorizedPersonCustomerID { get; set; }

        /// <summary>
        /// Strani kljuc ovlascenog lica
        /// </summary>
        public Guid AuthorizedPersonID { get; set; }

        /// <summary>
        /// Strani kljuc kupca
        /// </summary>
        public Guid CustomerID { get; set; }
    }
}
