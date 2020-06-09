using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("Food")]
    public partial class Food : Entity<long>
    {
        public long Id { get; set; }
        public long? Fcid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HeadImgUrl { get; set; }
        public string VideoUrl { get; set; }
        public string ProduceVideoUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsEnabled { get; set; }
    }
}
