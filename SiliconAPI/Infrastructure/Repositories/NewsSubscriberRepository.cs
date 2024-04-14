using Infrastructure.Contexts;
using Infrastructure.Entities;
using System.Diagnostics;

namespace Infrastructure.Repositories;

public class NewsSubscriberRepository(ApiContext apiContext) : Repository<NewsSubscriberEntity>(apiContext)
{
    public override async Task<NewsSubscriberEntity> UpdateAsync(NewsSubscriberEntity entity)
    {
        try
        {
            entity.DateUpdated = DateTime.Now;
            return await base.UpdateAsync(entity);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }
}
