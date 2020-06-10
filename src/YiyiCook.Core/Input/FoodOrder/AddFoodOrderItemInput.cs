using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Core.Input.FoodOrder
{
    public class AddFoodOrderItemInput
    {
        public long Fid { get; set; }
        public int Num { get; set; }
        public string Description { get; set; }
    }
}
