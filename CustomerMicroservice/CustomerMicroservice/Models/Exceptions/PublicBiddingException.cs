using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.Exceptions
{
    public class PublicBiddingException : Exception
    {
        public PublicBiddingException(string message) : base(message) { }
    }
}
