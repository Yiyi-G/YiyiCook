using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodClassfy")]
    public partial class FoodClassfy : Entity<long>, IHasCreationTime, IHasModificationTime
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
