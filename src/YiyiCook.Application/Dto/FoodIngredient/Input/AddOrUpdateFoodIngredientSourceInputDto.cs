using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.FoodIngredient.Input
{
    public class AddOrUpdateFoodIngredientSourceInputDto
    {
        public string name { get; set; }
        public string unit_name { get; set; }
    }
}
