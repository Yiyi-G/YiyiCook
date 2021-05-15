using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.FoodIngredient.Input
{
    public class AddUpdateAndDeleteFoodIngredientInputDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Num { get; set; }
        public string Description { get; set; }
    }
}
