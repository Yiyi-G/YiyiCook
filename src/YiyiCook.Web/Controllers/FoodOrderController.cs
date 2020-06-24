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
        [HttpPost]
        public async Task<IActionResult> AddFoodOrder([FromBody]AddFoodOrderInputDto input)
        {
            await _FoodOrderService.AddFoodOrder(input);
            return this.JsonApiResult(ErrorCode.None);
        }
        [HttpPost]
        public async Task<IActionResult> AddFoodOrderItem([FromBody]AddFoodOrderItemsInputDto input)
        {
            await _FoodOrderService.AddFoodOrderItem(input);
            return this.JsonApiResult(ErrorCode.None);
        }
        [HttpGet]
        public async Task<ActionResult<FoodOrderDto>> GetFootOrder([FromQuery]long foid)
        {
            var source = await _FoodOrderService.GetFootOrder(foid);
            return this.JsonApiResult(ErrorCode.None, new
            {
                order = source
            });
        }
        [HttpGet]
        public async Task<ActionResult<PageModel<FoodOrderDto>>> GetPageFootOrderList([FromQuery]SearchOrderQueryDto query)
        {
            var source = await _FoodOrderService.GetPageFootOrderList(query);
            return this.JsonApiResult(ErrorCode.None, new
            {
                orders = source
            });
        }
        [HttpPost]
        public async Task<IActionResult> SetOrderState([FromBody]UpdateOrderStateInputDto input)
        {
            await _FoodOrderService.SetOrderState(input);
            return this.JsonApiResult(ErrorCode.None);
        }
    }
}