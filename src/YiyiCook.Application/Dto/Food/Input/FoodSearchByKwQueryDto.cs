using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.Food.Input
{
    public class FoodSearchByKwQueryDto
    {
        public string kw { get; set; }
        public int start { get; set; }
        public int limit { get; set; }
    }
}
