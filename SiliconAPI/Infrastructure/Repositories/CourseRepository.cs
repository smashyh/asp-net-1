using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CourseRepository(ApiContext apiContext) : Repository<CourseEntity>(apiContext)
    {
        public override IQueryable<CourseEntity> GetSet(bool includeRelations)
        {
            if (includeRelations)
                return Context.Set<CourseEntity>()
                    .Include(x => x.CourseBadges)
                    .Include(x => x.CourseCreator)
                    .Include(x => x.Category);

            return base.GetSet(includeRelations);
        }
    }
}
