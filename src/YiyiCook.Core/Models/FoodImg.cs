using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodImg")]
    public partial class FoodImg : Entity<long>, IHasCreationTime
    {
        public long Id { get; set; }
        public long Fid { get; set; }
        public string Url { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreationTime { get; set; }
        public long FileId { get; set; }
    }
}
