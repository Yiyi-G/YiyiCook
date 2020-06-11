using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
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
    }
}
