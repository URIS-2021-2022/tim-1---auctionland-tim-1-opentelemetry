using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    public class UserTypeUpdateDto
    {
        public Guid UserTypeId { get; set; }

        public string UserTypeName { get; set; }

    }
}
