using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AddressService(AddressRepository repository, UserManager<UserEntity> userManager)
    {
        private readonly AddressRepository _repository = repository;
        private readonly UserManager<UserEntity> _userManager = userManager;

        public async Task<bool> CreateAsync(AddressEntity entity)
        {
            try
            {
                // Entity has to be completely unique, thus double user ownership
                // is not allowed. Also don't apply if id already exists.
                if (await _repository.ExistsAsync(x => x.Id == entity.Id))
                {
                    return false;
                }

                var result = await _repository.CreateAsync(entity);
                if (result != null)
                {
                    return true;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }

            return false;
        }

        /// <summary>
        /// Returns the first address associated with the provided user. If user is 
        /// null, or no address associated with this user could be found, returns null.
        /// </summary>
        public async Task<AddressEntity?> GetFirstAddressBelongingToUserAsync(UserEntity user)
        {
            try
            {
                if (user == null)
                    return null;

                // Make sure the user in question still exists in the databse.
                var userInDb = await _userManager.FindByIdAsync(user.Id);
                if (userInDb == null)
                    return null;

                var result = await _repository.GetAsync(x => x.UserId == userInDb.Id);
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex); }

            return null;
        }

        /// <summary>
        /// Returns a list of addresses belonging to provided user. If user is null, or no addresses
        /// associated with this user could be found, returns an empty list.
        /// </summary>
        public async Task<IEnumerable<AddressEntity>> GetAllAddressesBelongingToUserAsync(UserEntity user)
        {
            try
            {
                if (user != null)
                {
                    // Make sure the user in question still exists in the databse.
                    var userInDb = await _userManager.FindByIdAsync(user.Id);
                    if (userInDb != null)
                    {
                        var result = await _repository.GetAllAsync(x => x.UserId == userInDb.Id);

                        if (result != null)
                            return result;
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }

            return new List<AddressEntity>();
        }

        public async Task<AddressEntity?> GetAddressAsync(Expression<Func<AddressEntity, bool>> expression)
        {
            try
            {
                var result = await _repository.GetAsync(expression);

                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex); }

            return null!;
        }

        public async Task<IEnumerable<AddressEntity>> GetAllAddressesAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync();

                return result ?? null!;
            }
            catch (Exception ex) { Debug.WriteLine(ex); }

            return null!;
        }

        public async Task<IEnumerable<AddressEntity>> GetAllAddressesAsync(Expression<Func<AddressEntity, bool>> expression)
        {
            try
            {
                var result = await _repository.GetAllAsync(expression);

                return result ?? null!;
            }
            catch (Exception ex) { Debug.WriteLine(ex); }

            return null!;
        }

        public async Task<bool> UpdateAddressAsync(AddressEntity address)
        {
            try
            {
                if (address != null)
                {
                    var result = await _repository.UpdateAsync(address);

                    if (result != null)
                        return true;
                }
            }
            catch(Exception ex) { Debug.WriteLine(ex); }
            
            return false;
        }

        public async Task<bool> DeleteAddressAsync(AddressEntity address)
        {
            try
            {
                if (address != null)
                {
                    bool result = await _repository.DeleteAsync(address);

                    return result;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }

            return false;
        }
    }
}
