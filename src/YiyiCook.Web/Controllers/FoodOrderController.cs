using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TgnetAbp.Api;
using TgnetAbp.Data;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto.FoodOrder;
using YiyiCook.Application.Dto.FoodOrder.Input;

namespace YiyiCook.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FoodOrderController :YiyiCookControllerBase
    {
        private readonly IFoodOrderService _FoodOrderService; 
        public FoodOrderController(IFoodOrderService foodOrderService)
        {
            _FoodOrderService = foodOrderService;
        }
        public async Task<IActionResult> AddFoodOrder(AddFoodOrderInputDto input)
        {
            await _FoodOrderService.AddFoodOrder(input);
            return this.JsonApiResult(ErrorCode.None);
        }
        public async Task<IActionResult> AddFoodOrderItem(AddFoodOrderItemsInputDto input)
        {
            await _FoodOrderService.AddFoodOrderItem(input);
            return this.JsonApiResult(ErrorCode.None);
        }
        public async Task<ActionResult<FoodOrderDto>> GetFootOrder(long foid)
        {
            var source = await _FoodOrderService.GetFootOrder(foid);
            return this.JsonApiResult(ErrorCode.None, new
            {
                order = source
            });
        }
        public async Task<ActionResult<PageModel<FoodOrderDto>>> GetPageFootOrderList(SearchOrderQueryDto query)
        {
            var source = await _FoodOrderService.GetPageFootOrderList(query);
            return this.JsonApiResult(ErrorCode.None, new
            {
                orders = source
            });
        }

        public async Task<IActionResult> SetOrderState(UpdateOrderStateInputDto input)
        {
            await _FoodOrderService.SetOrderState(input);
            return this.JsonApiResult(ErrorCode.None);
        }
    }
}