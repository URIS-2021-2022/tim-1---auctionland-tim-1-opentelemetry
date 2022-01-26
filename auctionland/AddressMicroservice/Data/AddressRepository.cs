using AddressMicroservice.Etities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressMicroservice.Data.Implementation
{
    public class AddressRepository : IAddressRepository
    {
        public static List<Address> addresses { get; set; } = new List<Address>();

        public AddressRepository()
        {
            FillData();
        }

        private void FillData()
        {
            addresses.AddRange(new List<Address>
            {
                new Address
                {
                    AddressID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Street = "Devet Jugovica",
                    Number = 89,
                    ZipCode = "21203",
                    CityID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    CityName = "Veternik",
                    CountryID = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    CountryName = "Srbija"
                },
                new Address
                {
                    AddressID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Street = "Svetozara Miletica",
                    Number = 2,
                    ZipCode = "21203",
                    CityID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    CityName = "Veternik",
                    CountryID = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    CountryName = "Srbija"
                }
            }
                );
        }

        public AddressConfirmation CreateAddress(Address address)
        {
            address.AddressID = Guid.NewGuid();
            addresses.Add(address);

            Address newAddress = GetAddressById(address.AddressID);

            return new AddressConfirmation
            {
               AddressID = newAddress.AddressID,
               Street = newAddress.Street,
               Number = newAddress.Number,
               ZipCode = newAddress.ZipCode,
               CityName = newAddress.CityName,
               CountryName = newAddress.CountryName
            };
        }

        public void DeleteAddress(Guid addressID)
        {
            addresses.Remove(addresses.FirstOrDefault(e => e.AddressID == addressID));

        }

        public Address GetAddressById(Guid addressID)
        {
            return addresses.FirstOrDefault(e => e.AddressID== addressID);
        }

        public List<Address> GetAllAddresses()
        {
            return addresses;
        }

        public AddressConfirmation UpdateAddress(Address address)
        {
            Address updatedAddress = GetAddressById(address.AddressID);

            updatedAddress.Street = address.Street;
            updatedAddress.Number = address.Number;
            updatedAddress.ZipCode = address.ZipCode;
            updatedAddress.CityID = address.CityID;
            updatedAddress.CityName = address.CityName;
            updatedAddress.CountryID = address.CountryID;
            updatedAddress.CountryName = address.CountryName;

            return new AddressConfirmation
            {
                AddressID = updatedAddress.AddressID,
                Street = updatedAddress.Street,
                Number = updatedAddress.Number,
                ZipCode = updatedAddress.ZipCode,
                CityName = updatedAddress.CityName,
                CountryName = updatedAddress.CountryName,
            };
        }
    }
}
