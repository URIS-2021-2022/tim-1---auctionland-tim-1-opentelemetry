using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.Exceptions
{
    public class AddressException : Exception
    {
        public AddressException(string message) : base(message)
        {

        }
    }
}
