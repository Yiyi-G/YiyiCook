using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.FoodIngredient.Input
{
    public class AddUpdateAndDeleteFoodIngredientInputDto
    {
        public long Id { get; set; }
        public long Fiid { get; set; }
        public int Num { get; set; }
        public string Description { get; set; }
    }
}
