using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto.FoodClassfy;
using YiyiCook.Core.Services;

namespace YiyiCook.Application.Implements
{
    public class FoodClassfyService: IFoodClassfyService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IFoodClassfyDomainService _FoodClassfyDomainService;
        public FoodClassfyService(
            IObjectMapper objectMapper,
            IFoodClassfyDomainService foodClassfyDomainService)
        {
            _objectMapper = objectMapper;
            _FoodClassfyDomainService = foodClassfyDomainService;
        }

        public async Task<IEnumerable<FoodClassfyListItemDto>> GetAllClassfy()
        {
            var source = await _FoodClassfyDomainService.GetAllClassfies();
            return _objectMapper.Map<IEnumerable<FoodClassfyListItemDto>>(source);
        }
    }
}
