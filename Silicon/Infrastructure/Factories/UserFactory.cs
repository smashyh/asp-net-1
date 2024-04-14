using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    public static class UserFactory
    {
        public static UserEntity Create(string firstName, string lastName, string email)
        {
            try
            {
                return new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                };
            }
            catch { }

            return null!;
        }
    }
}
