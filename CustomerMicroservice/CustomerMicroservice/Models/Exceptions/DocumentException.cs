using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.Exceptions
{
    public class DocumentException : Exception
    {
        public DocumentException(string message) : base(message) { }
    }
}
