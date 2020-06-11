using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Core.Input.FoodIngredient
{
    public class SearchIngredientSourceQuery
    {
        public string Name { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
    }
}
