using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Infrastruction.Enum.FoodOrder;

namespace YiyiCook.Application.Dto.FoodOrder
{
    public class FoodOrderDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public FoodOrderType Type { get; set; }
        public FoodOrderState State { get; set; }
        public string Description { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public FoodOrderItemDto[] Item { get; set; }
    }
}
