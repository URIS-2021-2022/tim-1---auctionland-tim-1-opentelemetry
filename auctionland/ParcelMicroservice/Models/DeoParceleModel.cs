using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class DeoParceleModel
    {
        public Guid DeoParceleID { get; set; }
        public int PovrsinaDelaParcele { get; set; }
        public KlasaZemljistaModel KlasaZemljistaID { get; set; }
    }
}
