using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    public static class AddressFactory
    {
        public static AddressEntity? Create(UserEntity user, string address1 = "", string? address2 = "", string postalCode = "", string city = "")
        {
            try
            {
                return new AddressEntity
                {
                    UserId = user.Id,
                    User = user,
                    Address_1 = address1,
                    Address_2 = address2,
                    PostalCode = postalCode,
                    City = city,
                };
            }
            catch (Exception ex) { Debug.WriteLine(ex); }

            return null!;
        }
    }
}
