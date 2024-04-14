using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;

namespace Infrastructure.Services;

public class NewsSubscriberService(NewsSubscriberRepository newsSubscriberRepository)
{
    private readonly NewsSubscriberRepository _newsSubscriberRepository = newsSubscriberRepository;

    public async Task<HttpStatusCode> CreateAsync(NewsSubscriberModel model)
    {
        try
        {
            if (await _newsSubscriberRepository.ExistsAsync(x => x.Email == model.Email))
            {
                return HttpStatusCode.Conflict;
            }

            if (model.Subscriptions > NewsletterSubscriptions.Everything) 
            {
                return HttpStatusCode.BadRequest;
            }

            var entity = new NewsSubscriberEntity
            {
                Email = model.Email,
                Subscriptions = model.Subscriptions,
            };
            
            var result = await _newsSubscriberRepository.CreateAsync(entity);
            if (result != null)
            {
                return HttpStatusCode.Created;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return HttpStatusCode.InternalServerError;
    }

    public async Task<NewsSubscriberEntity?> GetAsync(Expression<Func<NewsSubscriberEntity, bool>> expression)
    {
        try
        {
            var entity = await _newsSubscriberRepository.GetAsync(expression);

            return entity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null;
    }

    public async Task<IEnumerable<NewsSubscriberEntity>> GetAllAsync(Expression<Func<NewsSubscriberEntity, bool>> expression)
    {
        try
        {
            var entities = await _newsSubscriberRepository.GetAllAsync(expression);

            return entities ?? new List<NewsSubscriberEntity>();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return new List<NewsSubscriberEntity>();
    }

    public async Task<bool> UpdateAsync(NewsSubscriberEntity entity)
    {
        try
        {
            if (entity == null)
                return false;

            entity.Email = entity.Email;
            entity.Subscriptions = entity.Subscriptions;

            // repo handles updating of DateUpdated.
            var result = await _newsSubscriberRepository.UpdateAsync(entity);
            return result != null;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(NewsSubscriberEntity entity)
    {
        try
        {
            if (entity == null)
                return false;

            var result = await _newsSubscriberRepository.DeleteAsync(entity);
            return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}
