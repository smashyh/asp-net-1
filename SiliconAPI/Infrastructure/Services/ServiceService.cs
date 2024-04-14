using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class ServiceService(ServiceRepository serviceRepository)
{
    private readonly ServiceRepository _serviceRepository = serviceRepository;

    public async Task<bool> AddServiceAsync(ServiceEntity entity)
    {
        return await _serviceRepository.CreateAsync(entity) != null;
    }

    public async Task<ServiceEntity?> GetServiceAsync(Expression<Func<ServiceEntity, bool>> expression, bool includeRelations = false)
    {
        return await _serviceRepository.GetAsync(expression, includeRelations);
    }

    public async Task<IEnumerable<ServiceEntity>> GetAllServicesAsync(bool includeRelations = false)
    {
        return await _serviceRepository.GetAllAsync(includeRelations);
    }

    public async Task<IEnumerable<ServiceEntity>> GetAllServicesAsync(Expression<Func<ServiceEntity, bool>> expression, bool includeRelations = false)
    {
        return await _serviceRepository.GetAllAsync(expression, includeRelations);
    }

    public async Task<bool> UpdateServiceAsync(ServiceEntity entity)
    {
        return await _serviceRepository.UpdateAsync(entity) != null;
    }

    public async Task<bool> DeleteServiceAsync(ServiceEntity entity)
    {
        return await _serviceRepository.DeleteAsync(entity);
    }

    public async Task<bool> IsValidServiceAsync(string serviceName)
    {
        return !string.IsNullOrEmpty(serviceName) && await GetServiceAsync(x => x.ServiceName == serviceName) != null;
    }

    public async Task<bool> ExistsAsync(string serviceName)
    {
        return await _serviceRepository.GetAsync(x => x.ServiceName == serviceName) != null;
    }
}
