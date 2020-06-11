using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Infrastruction.Enum.FoodOrder;

namespace YiyiCook.Application.Dto.FoodOrder.Input
{
    public class UpdateOrderStateInputDto
    {
        public long Foid { get; set; }
        public FoodOrderState State { get; set; }
    }
}
