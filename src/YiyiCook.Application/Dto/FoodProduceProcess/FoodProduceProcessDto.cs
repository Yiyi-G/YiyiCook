using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.FoodProduceProcess
{
    public class FoodProduceProcessDto
    {
        public long Id { get; set; }
        public long Fid { get; set; }
        public int RankNum { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public long[] ImgIds { get; set; }
    }
}
