using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    public class Exceptions : Exception
    {
        public Exceptions(string message) : base(message)
        {

        }
   
    }
}
