using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class CourseResultModel
    {
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public IEnumerable<CourseModel> Courses { get; set; } = new List<CourseModel>();
    }
}
