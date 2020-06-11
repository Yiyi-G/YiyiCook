using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Application.Dto.Food;

namespace YiyiCook.Application.Dto.FoodOrder
{
    public class FoodOrderItemDto
    {
        public long Id { get; set; }
        public long Foid { get; set; }
        public FoodDto Food { get; set; }
        public int Num { get; set; }
    }
}
