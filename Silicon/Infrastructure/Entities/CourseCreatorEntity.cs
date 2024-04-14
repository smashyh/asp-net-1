using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class CourseCreatorEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public int YouTubeSubscriberCount { get; set; }
        public int FacebookFollowerCount { get; set; }
        // @todo: Profile pic
    }
}
