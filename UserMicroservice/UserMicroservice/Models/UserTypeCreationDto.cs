﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje tipa korisnika
    /// </summary>
    public class UserTypeCreationDto
    {
        public Guid UserTypeId { get; set; }

        public string UserTypeName { get; set; }

    }
}
