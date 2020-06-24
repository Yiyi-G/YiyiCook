using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TgnetAbp.Api;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto.FoodClassfy;

namespace YiyiCook.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FoodClassfyController : YiyiCookControllerBase
    {
        private readonly IFoodClassfyService _FoodClassfyService;
        public FoodClassfyController(IFoodClassfyService foodClassfyService)
        {
            _FoodClassfyService = foodClassfyService;
        }
        [HttpGet]
        public async Task<ActionResult<FoodClassfyListItemDto[]>> GetAllClassfy()
        {
            var classfies = await _FoodClassfyService.GetAllClassfy();
            return this.JsonApiResult(ErrorCode.None, new
            {
                classfies = classfies
            });
        }
    }
}