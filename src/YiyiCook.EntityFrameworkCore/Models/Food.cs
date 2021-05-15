using System;
using System.Collections.Generic;

namespace YiyiCook.Models
{
    public partial class Food
    {
        public long Id { get; set; }
        public long? Fcid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HeadImgUrl { get; set; }
        public string VideoUrl { get; set; }
        public string ProduceVideoUrl { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual FoodImg FoodImg { get; set; }
    }
}
