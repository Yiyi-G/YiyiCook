using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using YiyiCook.Infrastruction.Enum.FoodOrder;

namespace YiyiCook.Core.Models
{
    [Table("FoodOrder")]
    public partial class FoodOrder : Entity<long>, IHasCreationTime, IHasModificationTime
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public FoodOrderType Type { get; set; }
        public FoodOrderState State { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
