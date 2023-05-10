using System;
using System.Collections.Generic;

namespace ChetanSoniAssignmentAgileRecruiTech.DbModels
{
    public partial class TblEvent
    {
        public TblEvent()
        {
            TblAttendees = new HashSet<TblAttendee>();
        }

        public int EventId { get; set; }
        public string? Type { get; set; }
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public string? Tagline { get; set; }
        public DateTime? Schedule { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int? Moderator { get; set; }
        public int? Category { get; set; }
        public int? SubCategory { get; set; }
        public int? RigorRank { get; set; }

        public virtual TblCategory? CategoryNavigation { get; set; }
        public virtual TblUser Event { get; set; } = null!;
        public virtual TblUser? ModeratorNavigation { get; set; }
        public virtual TblCategory? SubCategoryNavigation { get; set; }
        public virtual ICollection<TblAttendee> TblAttendees { get; set; }
    }
}
