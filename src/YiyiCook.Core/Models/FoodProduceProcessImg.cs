using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodProduceProcessImg")]
    public partial class FoodProduceProcessImg : Entity<long>
    {
        public long Id { get; set; }
        public long Fppid { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsEnabled { get; set; }
    }
}
