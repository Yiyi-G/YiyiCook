using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Application.Dto.FoodClassfy;

namespace YiyiCook.Application.Dto.Food
{
    public class FoodListItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HeadImgUrl { get; set; }
        public string VideoUrl { get; set; }
        public string ProduceVideoUrl { get; set; }
    }
}
