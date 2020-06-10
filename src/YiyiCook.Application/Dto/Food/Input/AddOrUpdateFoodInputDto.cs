using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.Food.Input
{
    public class AddOrUpdateFoodInputDto
    {
        public long Id { get; set; }
        public long? Fcid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HeadImgUrl { get; set; }
        public string VideoUrl { get; set; }
        public string ProduceVideoUrl { get; set; }
        public string[] FoodImages { get; set; }
        
    }
}
