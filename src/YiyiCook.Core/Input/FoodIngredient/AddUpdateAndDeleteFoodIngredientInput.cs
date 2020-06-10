using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Core.Input.FoodIngredient
{
    public class AddUpdateAndDeleteFoodIngredientInput
    {
        public long Id { get; set; }
        public long Fiid { get; set; }
        public int Num { get; set; }
        public string Description { get; set; }
    }
}
