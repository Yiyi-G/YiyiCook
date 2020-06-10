using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Infrastruction.Enum.FoodOrder;

namespace YiyiCook.Core.Input.FoodOrder
{
    public class AddFoodOrderInput
    {
        public DateTime Date { get; set; }
        public FoodOrderType Type { get; set; }
        public string Description { get; set; }
        public AddFoodOrderItemInput[] Items { get; set; }
    }
}
