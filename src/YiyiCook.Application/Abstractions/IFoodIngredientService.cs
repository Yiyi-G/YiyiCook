using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YiyiCook.Application.Dto.FoodIngredient;
using YiyiCook.Application.Dto.FoodIngredient.Input;

namespace YiyiCook.Application.Abstractions
{
    public interface IFoodIngredientService : Abp.Application.Services.IApplicationService
    {
        Task<FoodIngredientSourceDto> AddOrUpdateIngredientSource(AddOrUpdateFoodIngredientSourceInputDto input);
        Task<IEnumerable<IngredientSourceSearchItemDto>> SearchIngredientSource(SearchIngredientSourceQueryDto query);
        Task AddUpdateAndDeleteFoodIngredients(AddUpdateAndDeleteFoodIngredientsInputDto input);
        Task<IEnumerable<FoodIngredientDto>> GetFoodIngredients(long[] fids);
    }
}
