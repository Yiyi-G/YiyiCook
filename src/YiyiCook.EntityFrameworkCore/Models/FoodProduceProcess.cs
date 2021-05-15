using System;
using System.Collections.Generic;

namespace YiyiCook.Models
{
    public partial class FoodProduceProcess
    {
        public long Id { get; set; }
        public long Fid { get; set; }
        public int RankNum { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
