using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.FoodIngredient.Input
{
    public class AddUpdateAndDeleteFoodIngredientsInputDto
    {
        public long Fid { get; set; }
        public AddUpdateAndDeleteFoodIngredientInputDto[] Ingredients { get; set; }
    }
}
