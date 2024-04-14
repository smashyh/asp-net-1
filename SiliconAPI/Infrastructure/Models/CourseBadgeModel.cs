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
        public CourseBadgeModel() { }

        public CourseBadgeModel(string badgeLabel, string backgroundColorStyling, string colorStyling, bool important)
        {
            BadgeLabel = badgeLabel;
            BackgroundColorStyling = backgroundColorStyling;
            ColorStyling = colorStyling;
            Important = important;
        }

        public CourseBadgeModel(CourseBadgeEntity courseBadgeEntity) 
        { 
            BadgeLabel = courseBadgeEntity.BadgeLabel;
            BackgroundColorStyling = courseBadgeEntity.BackgroundColorStyling;
            ColorStyling = courseBadgeEntity.ColorStyling;
            Important = courseBadgeEntity.Important;
        }

        public string BadgeLabel { get; set; } = null!;
        public string BackgroundColorStyling { get; set; } = null!;
        public string ColorStyling { get; set; } = null!;
        public bool Important { get; set; }
    }
}
