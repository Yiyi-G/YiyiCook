using System;
using System.Collections.Generic;
using System.Text;
using TgnetAbp;

namespace YiyiCook.Core.Input.FoodIngredient
{
    public class AddOrUpdateFoodIngredientSourceInput
    {
        public string Name { get; set; }
        public string UnitName { get; set; }
        public void Verify()
        {
            ExceptionHelper.ThrowIfNullOrWhiteSpace(Name, nameof(Name));
            ExceptionHelper.ThrowIfNullOrWhiteSpace(UnitName, nameof(UnitName));
        }
    }
}
