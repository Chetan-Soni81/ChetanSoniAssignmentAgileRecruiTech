using System;
using System.Collections.Generic;

namespace ChetanSoniAssignmentAgileRecruiTech.DbModels
{
    public partial class TblNudge
    {
        public int NudgeId { get; set; }
        public int? EventId { get; set; }
        public string Title { get; set; } = null!;
        public string? Image { get; set; }
        public DateTime? Schedule { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? InvitationMessage { get; set; }

        public virtual TblEvent? Event { get; set; }
    }
}
