using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.FoodIngredient.Input
{
    public class SearchIngredientSourceQueryDto
    {
        public string Name { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
    }
}
