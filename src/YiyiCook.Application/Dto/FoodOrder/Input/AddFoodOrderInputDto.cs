using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Infrastruction.Enum.FoodOrder;

namespace YiyiCook.Application.Dto.FoodOrder.Input
{
    public class AddFoodOrderInputDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public FoodOrderType Type { get; set; }
        public string Description { get; set; }
        public AddFoodOrderItemInputDto[] Items { get; set; }
    }
}
