﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    /// <summary>
    /// Kreiranje DTO za mikroservis oglas.
    /// </summary>
    public class AdDto2
    {
            /// <summary>
            /// Naziv oglasa
            /// </summary>
            public string AdName { get; set; }

            #region officialList 

            /// <summary>
            /// ID službenog lista
            /// </summary>
            public Guid OfficialListID { get; set; }

            /// <summary>
            /// Naziv opštine
            /// </summary>
            public string MunicipalityName { get; set; }

            /// <summary>
            /// Broj službenog lista
            /// </summary>
            public string OfficialListNumber { get; set; }

            /// <summary>
            /// Datum izdavanja službenog lista
            /// </summary>
            public DateTime DateOfIssue { get; set; }

            #endregion
        }
    }

