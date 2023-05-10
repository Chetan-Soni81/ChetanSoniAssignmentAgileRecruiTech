using System;
using System.Collections.Generic;

namespace ChetanSoniAssignmentAgileRecruiTech.DbModels
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblEventCategoryNavigations = new HashSet<TblEvent>();
            TblEventSubCategoryNavigations = new HashSet<TblEvent>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<TblEvent> TblEventCategoryNavigations { get; set; }
        public virtual ICollection<TblEvent> TblEventSubCategoryNavigations { get; set; }
    }
}
