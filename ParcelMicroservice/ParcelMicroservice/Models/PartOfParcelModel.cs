﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Models
{
    public class PartOfParcelModel
    {
   
        public Guid ParcelID { get; set; }
        public Guid PartOfParcelID { get; set; }
        public int SurfaceAreaPOP { get; set; }

        #region Land Class
        public Guid ClassID { get; set; }
        public string ClassLandLabel { get; set; }

        #endregion
    }
}
