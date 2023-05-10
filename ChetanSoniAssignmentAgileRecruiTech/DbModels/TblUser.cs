using System;
using System.Collections.Generic;

namespace ChetanSoniAssignmentAgileRecruiTech.DbModels
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblAttendees = new HashSet<TblAttendee>();
            TblEventModeratorNavigations = new HashSet<TblEvent>();
        }

        public int UserId { get; set; }
        public string? Name { get; set; }

        public virtual TblEvent? TblEventEvent { get; set; }
        public virtual ICollection<TblAttendee> TblAttendees { get; set; }
        public virtual ICollection<TblEvent> TblEventModeratorNavigations { get; set; }
    }
}
