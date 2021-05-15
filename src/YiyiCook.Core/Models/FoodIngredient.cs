using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodIngredient")]
    public partial class FoodIngredient : Entity<long>
    {
        public string Name { get; set; }
        public long Fid { get; set; }
        public string Num { get; set; }
        public string Description { get; set; }
    }
}
