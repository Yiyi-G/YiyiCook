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
        private readonly IFileService _FileService;

        public FoodProduceProcessController(IFoodProduceProcessService foodProduceProcessService,
            IFileService fileService)
        {
            _FoodProduceProcessService = foodProduceProcessService;
            _FileService = fileService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdateAndDeleteFoodProduceProcess([FromBody]AddUpdateAndDeleteFoodProduceProcessesInputDto input)
        {
            if (input.ProduceProcesses != null && input.ProduceProcesses.Any())
            {
                foreach (var process in input.ProduceProcesses)
                {
                    List<long> imgIds = new List<long>();
                    foreach (var item in process.ImgIds ?? new long[0])
                    {
                        imgIds.Add(await _FileService.SaveFile(item, "YiyiCook"));
                    }
                    process.ImgIds = imgIds.ToArray();
                }
            }
            await _FoodProduceProcessService.AddUpdateAndDeleteFoodProduceProcess(input);
            return this.JsonApiResult(ErrorCode.None);
        }
        [HttpGet]
        public async Task<IActionResult> GetFoodProduceProcesses([FromQuery] long fid)
        {
            var source = await _FoodProduceProcessService.GetFoodProduceProcess(fid);
            return this.JsonApiResult(ErrorCode.None, new
            {
                processes = source
            });
        }
    }
}