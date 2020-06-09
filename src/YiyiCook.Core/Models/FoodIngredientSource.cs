using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodIngredientSource")]
    public partial class FoodIngredientSource : Entity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
    }
}
