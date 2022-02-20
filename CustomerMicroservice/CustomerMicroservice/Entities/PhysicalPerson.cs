﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Entities
{
    public class PhysicalPerson : Customer
    {
        /// <summary>
        /// ID fizickog lica
        /// </summary>
        [Key]
        public Guid PhysicalPersonID { get; set; }

        /// <summary>
        /// JMBG fizickog lica
        /// </summary>
        public string JMBG { get; set; }

        /// <summary>
        /// Ime fizickog lica
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime fizickog lica
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Broj telefona 1 fizickog lica
        /// </summary>
        public string PhoneNumber1 { get; set; }

        /// <summary>
        /// Broj telefona 2 fizickog lica
        /// </summary>
        public string PhoneNumber2 { get; set; }

        /// <summary>
        /// Email fizickog lica 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Broj racuna fizickog lica
        /// </summary>
        public string AccountNumber { get; set; }
    }
}
