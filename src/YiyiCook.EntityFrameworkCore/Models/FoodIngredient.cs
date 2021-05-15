using System;
using System.Collections.Generic;

namespace YiyiCook.Models
{
    public partial class FoodIngredient
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Fid { get; set; }
        public string Num { get; set; }
        public string Description { get; set; }
    }
}
