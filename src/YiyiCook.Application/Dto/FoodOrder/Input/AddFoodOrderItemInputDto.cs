using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.FoodOrder.Input
{
    public class AddFoodOrderItemInputDto
    {
        public long Fid { get; set; }
        public int Num { get; set; }
        public string Description { get; set; }
    }
}
