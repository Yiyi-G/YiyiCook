using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.FoodOrder.Input
{
    public class AddFoodOrderItemsInputDto
    {
        public long Foid { get; set; }
        public AddFoodOrderItemInputDto[] Items { get; set; }
    }
}
