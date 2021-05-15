using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Core.Input.FoodProduceProcess
{
    public class AddUpdateAndDeleteFoodProduceProcessInput
    {
        public long Id { get; set; }
        public long Fid { get; set; }
        public int RankNum { get; set; }
        public string Description { get; set; }
        public long[] ImgIds { get; set; }
    }
}
