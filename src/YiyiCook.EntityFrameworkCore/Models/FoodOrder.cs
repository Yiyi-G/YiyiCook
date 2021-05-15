using System;
using System.Collections.Generic;

namespace YiyiCook.Models
{
    public partial class FoodOrder
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public byte Type { get; set; }
        public byte State { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
