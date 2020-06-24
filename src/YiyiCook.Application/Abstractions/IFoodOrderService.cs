﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TgnetAbp.Data;
using YiyiCook.Application.Dto.FoodOrder;
using YiyiCook.Application.Dto.FoodOrder.Input;

namespace YiyiCook.Application.Abstractions
{
    public interface IFoodOrderService : Abp.Application.Services.IApplicationService
    {
        Task AddFoodOrder(AddFoodOrderInputDto input);
        Task AddFoodOrderItem(AddFoodOrderItemsInputDto input);
        Task<FoodOrderDto> GetFootOrder(long foid);
        Task<PageModel<FoodOrderDto>> GetPageFootOrderList(SearchOrderQueryDto query);
        Task SetOrderState(UpdateOrderStateInputDto input);
    }
}
