using Infrastructure.Entities;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    public static class CourseBadgeFactory
    {
        public static CourseBadgeModel Create(CourseBadgeEntity entity)
        {
            var model = new CourseBadgeModel(entity);

            return model;
        }

        public static IEnumerable<CourseBadgeModel> Create(IEnumerable<CourseBadgeEntity> entities)
        {
            var models = new List<CourseBadgeModel>();
            foreach (var entity in entities)
            {
                models.Add(Create(entity));
            }

            return models;
        }
    }
}
