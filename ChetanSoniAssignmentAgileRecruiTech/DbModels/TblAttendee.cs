using System;
using System.Collections.Generic;

namespace ChetanSoniAssignmentAgileRecruiTech.DbModels
{
    public partial class TblAttendee
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? EventId { get; set; }

        public virtual TblEvent? Event { get; set; }
        public virtual TblUser? User { get; set; }
    }
}
