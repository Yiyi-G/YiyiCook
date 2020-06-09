using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodClassfy")]
    public partial class FoodClassfy : Entity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsEnabled { get; set; }
    }
}
