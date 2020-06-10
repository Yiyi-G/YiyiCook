using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodProduceProcess")]
    public partial class FoodProduceProcess : Entity<long>, IHasCreationTime, IHasModificationTime
    {
        public long Id { get; set; }
        public long Fid { get; set; }
        public int RankNum { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
