using System;
using System.Collections.Generic;

namespace YiyiCook.Models
{
    public partial class FoodOrderItem
    {
        public long Id { get; set; }
        public long Foid { get; set; }
        public long Fid { get; set; }
        public int Num { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
