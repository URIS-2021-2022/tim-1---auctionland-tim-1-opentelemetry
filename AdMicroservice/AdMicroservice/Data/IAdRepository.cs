using AdMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data
{
    /// <summary>
    /// Kreiran interfejs IAdRepository
    /// </summary>
    public interface IAdRepository
    {
        /// <summary>
        /// Metoda koja vraća listu oglasa
        /// </summary>
        /// <param name="municipalityName"></param>
        /// <returns></returns>
        List<Ad> GetAds(string municipalityName);
        /// <summary>
        /// Metoda koja vraća oglas sa prosleđenim ID-jem
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        Ad GetAdById(Guid adId);
        /// <summary>
        /// Metoda koja kreira oglas na osnovu prosleđenog modela 
        /// </summary>
        /// <param name="adModel"></param>
        /// <returns></returns>
        AdConfirmation CreateAd(Ad adModel);
        /// <summary>
        /// Metoda koja ažurira oglas
        /// </summary>
        /// <param name="adModel"></param>
        /// <returns></returns>
        void UpdateAd(Ad adModel);
        /// <summary>
        /// Metoda koja briše konkretni oglas
        /// </summary>
        /// <param name="adId"></param>
        void DeleteAd(Guid adId);

        /// <summary>
        /// Čuva promene 
        /// </summary>
        bool SaveChanges();
    }
}
