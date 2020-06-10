using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Core.Input.FoodIngredient
{
    public class AddOrUpdateFoodIngredientSourceInput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
    }
}
