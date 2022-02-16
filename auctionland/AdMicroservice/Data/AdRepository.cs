using AdMicroservice.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data
{
    /// <summary>
    /// Klasa koja implementira metode interfejsa IAdRepository
    /// </summary>
    public class AdRepository : IAdRepository
    {
        private readonly AdContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Kreiran konstruktor koji vrši injektovanje zavisnosti 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public AdRepository(AdContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Kreirana metoda koja čuva promene
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        /// <summary>
        /// Kreirana metoda koja na osnovu prosleđenog modela kreira oglas
        /// </summary>
        /// <param name="adModel"></param>
        /// <returns></returns>
        public AdConfirmation CreateAd(Ad adModel)
        {
            var createEntity = context.Add(adModel);
            return mapper.Map<AdConfirmation>(createEntity.Entity);
        }

        /// <summary>
        /// Kreirana metoda koja na osnovu prosleđenog ID-ja briše oglas
        /// </summary>
        /// <param name="adId"></param>
        public void DeleteAd(Guid adId)
        {
            var registration = GetAdById(adId);
            context.Remove(registration);
        }

        /// <summary>
        /// Kreirana metoda koja na osnovu prosleđenog ID-ja prikazuje oglas
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        public Ad GetAdById(Guid adId)
        {
            return context.Ads.FirstOrDefault(e => e.AdID == adId);
        }

        /// <summary>
        /// Kreirana metoda koja vraća listu oglasa i na osnovu koje može da se radi filtriranje liste na osnovu parametra
        /// </summary>
        /// <param name="municipalityName"></param>
        /// <returns></returns>
        public List<Ad> GetAds(string municipalityName)
        {
            return context.Ads.Where(a => string.IsNullOrEmpty(municipalityName) || a.MunicipalityName == municipalityName).ToList();
        }

        /// <summary>
        /// Kreirana metoda za ažuriranje oglasa, koja je zagomentarisana jer postoji metoda SaveChanges
        /// </summary>
        /// <param name="adModel"></param>
        /// <returns></returns>
        public AdConfirmation UpdateAd(Ad adModel)
        {
            throw new NotImplementedException();
        }
    }
}
