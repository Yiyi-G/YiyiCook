using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.Food.Input
{
    public class AddOrUpdateFoodInputDto
    {
        public long Id { get; set; }
        public long? Fcid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long[] foodImgIds { get; set; }


    }
}
