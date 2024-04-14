using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class CourseBadgeModel
    {
        public string BadgeLabel { get; set; } = null!;
        public string BackgroundColorStyling { get; set; } = null!;
        public string ColorStyling { get; set; } = null!;
        public bool Important { get; set; }
    }
}
