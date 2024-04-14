using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AddressRepository(DataContext dataContext) : Repository<AddressEntity>(dataContext)
    {
        protected override IQueryable<AddressEntity> GetSet(bool includeRelations)
        {
            if (includeRelations)
                return Context.Set<AddressEntity>().Include(x => x.User);

            return base.GetSet(includeRelations);
        }
    }
}
