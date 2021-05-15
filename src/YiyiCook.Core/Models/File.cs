using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiyiCook.Core.Models
{
    [Table("File")]
    public partial class File : Entity<long>
    {
        public string FileName { get; set; }
        public bool IsTemp { get; set; }
    }
    
}
