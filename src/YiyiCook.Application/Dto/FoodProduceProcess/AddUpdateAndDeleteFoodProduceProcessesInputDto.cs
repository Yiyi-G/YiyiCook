using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.Application.Dto.FoodProduceProcess
{
    public class AddUpdateAndDeleteFoodProduceProcessesInputDto
    {
        public long Fid { get; set; }
        public AddUpdateAndDeleteFoodProduceProcessInputDto[] ProduceProcesses { get; set; }
    }
}
