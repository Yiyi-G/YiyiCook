using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodProduceProcess")]
    public partial class FoodProduceProcess : Entity<long>
    {
        public long Id { get; set; }
        public long Fid { get; set; }
        public int RankNum { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsEnabled { get; set; }
    }
}
