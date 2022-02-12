using AddressMicroservice.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Data
{
    public interface IAddressRepository
    {
        List<Address> GetAllAddresses();

        Address GetAddressById(Guid addressID);

        AddressConfirmation CreateAddress(Address address);

        void UpdateAddress(Address address);

        void DeleteAddress(Guid addressID);

        bool SaveChanges();
    }
}
