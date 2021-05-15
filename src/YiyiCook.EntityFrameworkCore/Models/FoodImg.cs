using System;
using System.Collections.Generic;

namespace YiyiCook.Models
{
    public partial class FoodImg
    {
        public long Id { get; set; }
        public long Fid { get; set; }
        public string Url { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime Created { get; set; }
        public long FileId { get; set; }

        public virtual Food IdNavigation { get; set; }
    }
}
