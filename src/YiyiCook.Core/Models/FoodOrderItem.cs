using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodOrderItem")]
    public partial class FoodOrderItem : Entity<long>
    {
        public long Id { get; set; }
        public long Foid { get; set; }
        public long Fid { get; set; }
        public byte State { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
