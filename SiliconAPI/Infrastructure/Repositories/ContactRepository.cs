using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContactRepository(ApiContext apiContext) : Repository<ContactEntity>(apiContext)
{
    public override IQueryable<ContactEntity> GetSet(bool includeRelations)
    {
        if (includeRelations)
            return Context.Set<ContactEntity>()
                .Include(x => x.Service);

        return base.GetSet(includeRelations);
    }
}
