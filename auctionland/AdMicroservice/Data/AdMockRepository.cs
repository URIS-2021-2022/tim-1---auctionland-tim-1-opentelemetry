using AdMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data
{
    /// <summary>
    /// Kreirana klasa koja implementira interfejs IAdRepository
    /// </summary>
    public class AdMockRepository : IAdRepository
    {
        /// <summary>
        /// Kreirana statička lista 
        /// </summary>
        public static List<Ad> Ads { get; set; } = new List<Ad>();

        /// <summary>
        /// Kreiran konstruktor 
        /// </summary>
        public AdMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Ads.AddRange(new List<Ad>
            {
                new Ad
                {
                    AdID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    AdName = "Prodaja placa kod pijace",
                    OfficialListID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    MunicipalityName = "Čantavir",
                    OfficialListNumber = "1/2022",
                    DateOfIssue = DateTime.Parse("2020-11-15T09:00:00")
                },

                new Ad
                {
                    AdID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    AdName = "Prodaja placa pored crkve",
                    OfficialListID = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    MunicipalityName = "Žednik",
                    OfficialListNumber = "2/2022",
                    DateOfIssue = DateTime.Parse("2020-11-16T09:00:00")
                }
            }); 
        }

        /// <summary>
        /// Kreirana metoda koja na osnovu prosleđenog modela kreira oglas
        /// </summary>
        /// <param name="adModel"></param>
        /// <returns></returns>
        public AdConfirmation CreateAd(Ad adModel)
        {
            //New Guid generisace novi guid 
            adModel.AdID = Guid.NewGuid();
            Ads.Add(adModel);
            Ad model = GetAdById(adModel.AdID);

            return new AdConfirmation
            {
                AdID = model.AdID,
                OfficialListID = model.OfficialListID,
                DateOfIssue = model.DateOfIssue
            };
        }

        /// <summary>
        /// Kreirana metoda koja na osnovu prosleđenog ID-ja briše oglas
        /// </summary>
        /// <param name="adId"></param>
        public void DeleteAd(Guid adId)
        {
            Ads.Remove(Ads.FirstOrDefault(e => e.AdID == adId));
        }

        /// <summary>
        /// Kreirana metoda koja na osnovu prosleđenog ID-ja prikazuje oglas
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        public Ad GetAdById(Guid adId)
        {
            return Ads.FirstOrDefault(e => e.AdID == adId);
        }

        /// <summary>
        /// Kreirana metoda koja vraća listu oglasa i na osnovu koje može da se radi filtriranje liste na osnovu parametra
        /// </summary>
        /// <param name="municipalityName"></param>
        /// <returns></returns>
        public List<Ad> GetAds(string municipalityName)
        {
            return (from a in Ads
                    where string.IsNullOrEmpty(municipalityName) || a.MunicipalityName == municipalityName
                    select a).ToList();
        }

        /// <summary>
        /// Kreirana metoda za ažuriranje oglasa
        /// </summary>
        /// <param name="adModel"></param>
        /// <returns></returns>
        public AdConfirmation UpdateAd(Ad adModel)
        {
            Ad model = GetAdById(adModel.AdID);

            model.AdID = adModel.AdID;
            model.AdName = adModel.AdName;
            model.OfficialListID = adModel.OfficialListID;
            model.MunicipalityName = adModel.MunicipalityName;
            model.OfficialListNumber = adModel.OfficialListNumber;
            model.DateOfIssue = adModel.DateOfIssue;

            return new AdConfirmation
            {
                AdID = model.AdID,
                OfficialListID = model.OfficialListID,
                DateOfIssue = model.DateOfIssue
            };

        }
    }
}
