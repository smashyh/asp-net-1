using Infrastructure.Entities;
using Infrastructure.Models;
namespace Infrastructure.Factories;

public static class CourseCreatorFactory
{
    public static CourseCreatorModel Create(CourseCreatorEntity entity)
    {
        var model = new CourseCreatorModel(entity);

        return model;
    }
}
