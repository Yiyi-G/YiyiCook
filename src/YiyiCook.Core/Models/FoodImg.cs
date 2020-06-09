using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("FoodImg")]
    public partial class FoodImg : Entity<long>
    {
        public long Id { get; set; }
        public long Fid { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public bool IsEnabled { get; set; }
    }
}
