using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodOrderItem")]
    public partial class FoodOrderItem : Entity<long>, IHasCreationTime, IHasModificationTime
    {
        public long Foid { get; set; }
        public long Fid { get; set; }
        public int Num { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
