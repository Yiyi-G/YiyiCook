using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Core.Input.FoodIngredient
{
    public class AddUpdateAndDeleteFoodIngredientInput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Num { get; set; }
        public string Description { get; set; }
    }
}
