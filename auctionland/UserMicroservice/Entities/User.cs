﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserMicroservice.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


        #region UserType
        public Guid UserTypeId { get; set; }

        public string UserTypeName { get; set; }
        #endregion

        public string Salt { get; set; }
    }
}