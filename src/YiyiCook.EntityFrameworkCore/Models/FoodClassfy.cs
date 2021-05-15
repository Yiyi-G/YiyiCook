using System;
using System.Collections.Generic;

namespace YiyiCook.Models
{
    public partial class FoodClassfy
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
