using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto.FoodProduceProcess;
using YiyiCook.Core.Input.FoodProduceProcess;
using YiyiCook.Core.Services;

namespace YiyiCook.Application.Implements
{
    public class FoodProduceProcessService : IFoodProduceProcessService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IFoodProduceProcessDomainService _FoodProduceProcessDomainService;
        public FoodProduceProcessService(
            IObjectMapper objectMapper,
            IFoodProduceProcessDomainService foodProduceProcessDomainService)
        {
            _objectMapper = objectMapper;
            _FoodProduceProcessDomainService = foodProduceProcessDomainService;
        }
        public async Task AddUpdateAndDeleteFoodProduceProcess(AddUpdateAndDeleteFoodProduceProcessesInputDto input)
        {
           await _FoodProduceProcessDomainService.AddUpdateAndDeleteFoodProduceProcess(input.Fid, _objectMapper.Map<AddUpdateAndDeleteFoodProduceProcessInput[]>(input.ProduceProcesses));
        }
        public async Task<FoodProduceProcessDto[]> GetFoodProduceProcess(long fid)
        {
            var processSources = await _FoodProduceProcessDomainService.GetFoodProduceProcess(fid);
            var fppids = processSources.Select(p => p.Id).ToArray();
            var imgs = await _FoodProduceProcessDomainService.GetFoodProduceProcessImgs(fppids);
            var processes = processSources.OrderBy(p=>p.RankNum).Select(p=> _objectMapper.Map<FoodProduceProcessDto>(p)).ToArray() ;
            foreach (var item in processes)
            {
                item.ImgIds = imgs.Where(p => p.Fppid == item.Id).Select(p=>p.FileId).ToArray();
            }
            return processes;
        }

    }
}
