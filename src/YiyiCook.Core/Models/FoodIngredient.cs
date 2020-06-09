using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodIngredient")]
    public partial class FoodIngredient : Entity<long>
    {
        public long Id { get; set; }
        public long Fiid { get; set; }
        public long Fid { get; set; }
        public int Num { get; set; }
        public string Description { get; set; }
    }
}
