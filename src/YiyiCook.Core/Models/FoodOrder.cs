using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodOrder")]
    public partial class FoodOrder : Entity<long>
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public byte Type { get; set; }
        public byte State { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
