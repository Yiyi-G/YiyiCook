using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TgnetAbp;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto.FoodIngredient;
using YiyiCook.Application.Dto.FoodIngredient.Input;
using YiyiCook.Core.Input.FoodIngredient;
using YiyiCook.Core.Models;
using YiyiCook.Core.Services;
using YiyiCook.Infrastruction.Utility;

namespace YiyiCook.Application.Implements
{
    public class FoodIngredientService : IFoodIngredientService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IFoodIngredientDomainService _FoodIngredientDomainService;
        private readonly IFoodOrderDomainService _FoodOrderDomainService;
        public FoodIngredientService(
            IObjectMapper objectMapper,
            IFoodIngredientDomainService foodIngredientDomainService,
            IFoodOrderDomainService foodOrderDomainService)
        {
            _objectMapper = objectMapper;
            _FoodIngredientDomainService = foodIngredientDomainService;
            _FoodOrderDomainService = foodOrderDomainService;
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
            await _FoodIngredientDomainService.AddUpdateAndDeleteFoodIngredients(input.Fid, _objectMapper.Map<AddUpdateAndDeleteFoodIngredientInput[]>(input.Ingredients));
        }
        public async Task<IEnumerable<FoodIngredientDto>> GetFoodIngredients(long[] fids)
        {
            var ingredients = await _FoodIngredientDomainService.GetFoodIngredients(fids);
            return _objectMapper.Map<IEnumerable<FoodIngredientDto>>(ingredients);
        }
        public async Task<IEnumerable<FoodIngredientDto>> GetFoodOrderIngredients(long foid)
        {
            ExceptionHelper.ThrowIfNotId(foid, nameof(foid));
            var foods = await _FoodOrderDomainService.GetFootOrderItems(foid);
            var fids = foods.Select(p => p.Fid).ToArray();
            var ingredientDics = await _FoodIngredientDomainService.GetFoodIngredientsDic(fids);
            var ingredients = ingredientDics.SelectMany(p=>p.Value.Select(i=> new {
                Id = i.Id,
                Name = i.Name,
                Num  = i.Num,
                Count = (foods.Where(f=>f.Fid==p.Key).FirstOrDefault()??new FoodOrderItem()).Num,
            }));
            var spices = GetSpices();
            var seasonning = GetSeasoning();
            var ingredientDtos = ingredients.GroupBy(p => p.Name)
                .Select(p => new FoodIngredientDto
            {
                Id = p.First().Id,
                Name = p.Key,
                Num = ConbineIngredientNum(p.ToDictionary(p => p.Num, p => p.Count))
            }).OrderBy(p => spices.Any(s => p.Name.Contains(s)))
              .ThenBy(p => seasonning.Any(s => p.Name.Contains(s)));
            return ingredientDtos;
        }
        private string[] GetSpices() {
            return new string[] { "酱油", "蚝油", "盐", "油", "生抽", "醋", "料酒", "蒸鱼豉油","老抽" };
        }
        private string[] GetSeasoning() {
            return new string[] { "葱", "姜", "蒜", "八角", "桂皮", "干辣椒","芝麻" };
        }
        private string ConbineIngredientNum(Dictionary<string,int> nums)
        {
            var noMatchs = new List<string>();
            var countDic = new Dictionary<double, string>();
            foreach (var num in nums)
            {
                bool matchUnitSuccess;
                bool matchCountSuccess;
                string matchCountSuccessPartStr;
                string unit = "";
                var count = ParseUtility.MatchNum(num.Key, out matchCountSuccess, out matchCountSuccessPartStr);
                if (matchCountSuccess)
                {
                    unit = MatchUnit(num.Key, out matchUnitSuccess,matchCountSuccessPartStr);
                    if (matchUnitSuccess)
                    {
                        countDic.Add(count*num.Value, unit);
                        break;
                    }
                }
                countDic.Add(0, num.Key+"×"+num.Value);
            }
            var matchSucessCount = countDic.Where(p => p.Key != 0).GroupBy(p => p.Value).Select(p => new
            {
                num = p.Select(n => n.Key).Sum(),
                unit = p.Key
            });
            var matchArr = matchSucessCount.Select(p => p.num.ToString("#.#") + p.unit).ToArray();
            var nomatchArr = countDic.Where(p => p.Key == 0).Select(p => p.Value);
            var conbineArr = matchArr.Concat(nomatchArr);
            var result = string.Join("+", conbineArr);
            return result;
        }
        private string MatchUnit(string num,out bool success,string replaceStr="")
        {
            success = false;
            num = (num??"").Trim();
            var match = Regex.Match(num, "[\u4e00-\u9fa5]+");
            if (match.Success)
            {
                var unit = match.Groups[0].Value;
                if (!string.IsNullOrWhiteSpace(replaceStr))
                    unit = unit.Replace(replaceStr, "");
                if (unit != num)
                    success = true;
                return unit;
            }
            return num;
        }
    }
}
