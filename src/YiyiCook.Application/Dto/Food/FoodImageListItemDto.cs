using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.Food
{
    public class FoodImageListItemDto
    {
        public long Fid { get; set; }
        public ImageDto[] Images { get; set; }
    }
}
