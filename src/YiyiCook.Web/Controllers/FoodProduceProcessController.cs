using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TgnetAbp.Api;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto.FoodProduceProcess;

namespace YiyiCook.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FoodProduceProcessController : YiyiCookControllerBase
    {
        private readonly IFoodProduceProcessService _FoodProduceProcessService; 
        public FoodProduceProcessController(IFoodProduceProcessService foodProduceProcessService)
        {
            _FoodProduceProcessService = foodProduceProcessService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdateAndDeleteFoodProduceProcess([FromBody]AddUpdateAndDeleteFoodProduceProcessesInputDto input)
        {
            await _FoodProduceProcessService.AddUpdateAndDeleteFoodProduceProcess(input);
            return this.JsonApiResult(ErrorCode.None);
        }
    }
}