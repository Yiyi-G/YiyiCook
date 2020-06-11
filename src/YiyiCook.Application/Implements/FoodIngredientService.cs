using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto.FoodIngredient;
using YiyiCook.Application.Dto.FoodIngredient.Input;
using YiyiCook.Core.Input.FoodIngredient;
using YiyiCook.Core.Models;
using YiyiCook.Core.Services;

namespace YiyiCook.Application.Implements
{
    public class FoodIngredientService : IFoodIngredientService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IFoodIngredientDomainService _FoodIngredientDomainService;
        public FoodIngredientService(
            IObjectMapper objectMapper,
            IFoodIngredientDomainService foodIngredientDomainService)
        {
            _objectMapper = objectMapper;
            _FoodIngredientDomainService = foodIngredientDomainService;
        }
        public async Task<FoodIngredientSourceDto> AddOrUpdateIngredientSource(AddOrUpdateFoodIngredientSourceInputDto input)
        {
            var source = await _FoodIngredientDomainService.AddOrUpdateIngredientSource(_objectMapper.Map<AddOrUpdateFoodIngredientSourceInput>(input));
            return _objectMapper.Map<FoodIngredientSourceDto>(source);
        }
        public async Task<IEnumerable<IngredientSourceSearchItemDto>> SearchIngredientSource(SearchIngredientSourceQueryDto query)
        {
            var source = await _FoodIngredientDomainService.SearchIngredientSource(_objectMapper.Map<SearchIngredientSourceQuery>(query));
            return _objectMapper.Map<IEnumerable<IngredientSourceSearchItemDto>>(source);
        }
        public async Task AddUpdateAndDeleteFoodIngredients(AddUpdateAndDeleteFoodIngredientsInputDto input)
        {
            await _FoodIngredientDomainService.AddUpdateAndDeleteFoodIngredients(input.Fid,_objectMapper.Map<AddUpdateAndDeleteFoodIngredientInput[]>(input.Ingredients));
        }
        public async Task <IEnumerable<FoodIngredientDto>> GetFoodIngredients(long[] fids)
        {
           var ingredients =  await _FoodIngredientDomainService.GetFoodIngredients(fids);
            return _objectMapper.Map<IEnumerable<FoodIngredientDto>>(ingredients);
        }
    }
}
