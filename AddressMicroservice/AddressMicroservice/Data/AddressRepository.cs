using AddressMicroservice.Etities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressMicroservice.Data.Implementation
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AddressContext context;
        private readonly IMapper mapper;

        public AddressRepository(AddressContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public AddressConfirmation CreateAddress(Address address)
        {
            var createdEntity = context.Add(address);
            return mapper.Map<AddressConfirmation>(createdEntity.Entity);
        }

        public void DeleteAddress(Guid addressID)
        {
            var address = GetAddressById(addressID);
            context.Remove(address);
        }

        public Address GetAddressById(Guid addressID)
        {
            return context.Addresses.FirstOrDefault(e => e.AddressID == addressID);
        }

        public List<Address> GetAllAddresses(string countryName = null, string cityName = null)
        {
            return context.Addresses.Where(e => (countryName == null || e.CountryName == countryName) &&
                                                        (cityName == null || e.CityName == cityName)).ToList();
        }

        public void UpdateAddress(Address address)
        {
           //Implementacija nije neophodna
        }
    }
}
