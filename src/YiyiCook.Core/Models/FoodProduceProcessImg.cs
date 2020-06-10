using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodProduceProcessImg")]
    public partial class FoodProduceProcessImg : Entity<long>, IHasCreationTime, IHasModificationTime
    {
        public long Id { get; set; }
        public long Fppid { get; set; }
        public string Url { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
