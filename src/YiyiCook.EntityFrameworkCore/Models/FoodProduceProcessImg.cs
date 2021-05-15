using System;
using System.Collections.Generic;

namespace YiyiCook.Models
{
    public partial class FoodProduceProcessImg
    {
        public long Id { get; set; }
        public long Fppid { get; set; }
        public long FileId { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
