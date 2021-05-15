using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.FoodIngredient
{
    public class FoodIngredientDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Fid { get; set; }
        public string Num { get; set; }
        public string Description { get; set; }
    }
}
