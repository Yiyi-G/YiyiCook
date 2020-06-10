using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("Food")]
    public partial class Food : Entity<long>, IHasCreationTime, IHasModificationTime 
    {
        public long Id { get; set; }
        public long? Fcid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HeadImgUrl { get; set; }
        public string VideoUrl { get; set; }
        public string ProduceVideoUrl { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
