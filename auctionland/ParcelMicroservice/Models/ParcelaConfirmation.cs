using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class ParcelaConfirmation
    {
        public Guid ParcelaID { get; set; }
        public int PovrsinaParcele { get; set; }
        public string BrojParcele { get; set; }

    }
}
