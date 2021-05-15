using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgnetAbp;
using YiyiCook.Core.Input.FoodIngredient;
using YiyiCook.Core.IRepositories;
using YiyiCook.Core.Models;

namespace YiyiCook.Core.Services
{
    public interface IFoodIngredientDomainService : Abp.Domain.Services.IDomainService
    {
        Task<FoodIngredientSource> AddOrUpdateIngredientSource(AddOrUpdateFoodIngredientSourceInput input);
        Task<IEnumerable<FoodIngredientSource>> SearchIngredientSource(SearchIngredientSourceQuery query);
        Task AddUpdateAndDeleteFoodIngredients(long fid, AddUpdateAndDeleteFoodIngredientInput[] inputs);
        Task<IEnumerable<FoodIngredientSource>> GetIngredientSourceByIds(long[] ids);
        Task<IEnumerable<FoodIngredient>> GetFoodIngredients(long[] fids);
        Task<Dictionary<long, IEnumerable<FoodIngredient>>> GetFoodIngredientsDic(long[] fids);
    }
    public class FoodIngredientDomainService : IFoodIngredientDomainService
    {
        private readonly IUnitOfWorkManager _UnitOfWorkManager;
        private readonly IFoodIngredientRepository _FoodIngredientRepository;
        private readonly IFoodIngredientSourceRepository _FoodIngredientSourceRepository;

        public FoodIngredientDomainService(IUnitOfWorkManager unitOfWorkManager,
            IFoodIngredientRepository foodIngredientRepository,
            IFoodIngredientSourceRepository foodIngredientSourceRepository)
        {
            _UnitOfWorkManager = unitOfWorkManager;
            _FoodIngredientRepository = foodIngredientRepository;
            _FoodIngredientSourceRepository = foodIngredientSourceRepository;
        }

        public async Task<FoodIngredientSource> AddOrUpdateIngredientSource(AddOrUpdateFoodIngredientSourceInput input)
        {

            var source = _FoodIngredientSourceRepository.GetAll().Where(p => p.Name == input.Name).FirstOrDefault();
            if (source == null)
                source = new Models.FoodIngredientSource();
            source.Name = input.Name;
            source.UnitName = input.UnitName;
            await _FoodIngredientSourceRepository.InsertOrUpdateAsync(source);
            return source;
        }

        public async Task<IEnumerable<FoodIngredientSource>> SearchIngredientSource(SearchIngredientSourceQuery query)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _FoodIngredientSourceRepository.GetAll().Where(p => p.Name.Contains(query.Name)).OrderByDescending(p => p.Id).PageBy(query.Start, query.Limit).ToArray();
            });
        }
        public async Task AddUpdateAndDeleteFoodIngredients(long fid, AddUpdateAndDeleteFoodIngredientInput[] inputs)
        {
            ExceptionHelper.ThrowIfNull(inputs, nameof(inputs));
            inputs = inputs.Where(p => !string.IsNullOrWhiteSpace(p.Name) && !string.IsNullOrWhiteSpace(p.Num)).ToArray();
            var names = inputs.Select(p => p.Name).Distinct();
            await AddIngrediets(names);
            await _FoodIngredientRepository.GetAll().Where(p => p.Fid == fid && !names.Contains(p.Name)).BatchDeleteAsync();
            var needUpdateItems = _FoodIngredientRepository.GetAll().Where(p => p.Fid == fid && names.Contains(p.Name)).ToArray();
            foreach (var name in names)
            {
                var item = inputs.FirstOrDefault(p => p.Name == name);
                FoodIngredient ingredient = null;
                ingredient = needUpdateItems.Where(p => p.Name == item.Name).FirstOrDefault();
                if (ingredient == null)
                    ingredient = new FoodIngredient() { Fid = fid, Name = item.Name };
                ingredient.Num = item.Num;
                ingredient.Description = item.Description;
                await _FoodIngredientRepository.InsertOrUpdateAsync(ingredient);
            }
        }
        private async Task AddIngrediets(IEnumerable<string> names)
        {
            names = names.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct().ToArray();
            if (names.Any())
            {
                var existNames = _FoodIngredientSourceRepository.GetAll().Where(p => names.Contains(p.Name)).Select(p => p.Name).ToArray();
                var newNames = names.Where(p => !existNames.Contains(p)).ToArray();
                foreach (var name in newNames)
                {
                    await _FoodIngredientSourceRepository.InsertAsync(new FoodIngredientSource()
                    {
                        Name = name,
                    });
                }
            }
        }
        public async Task<IEnumerable<FoodIngredientSource>> GetIngredientSourceByIds(long[] ids)
        {
            return await Task.Factory.StartNew(() =>
            {
                ids = (ids ?? new long[0]).Where(p => p > 0).Distinct().ToArray();
                if (ids.Length == 0) return new FoodIngredientSource[0];
                return _FoodIngredientSourceRepository.GetAll().Where(p => ids.Contains(p.Id)).ToArray();
            });
        }
        public async Task<IEnumerable<FoodIngredient>> GetFoodIngredients(long[] fids)
        {
            return await Task.Factory.StartNew(() =>
            {
                fids = (fids ?? new long[0]).Where(p => p > 0).Distinct().ToArray();
                if (fids.Length == 0) return new FoodIngredient[0];
                return _FoodIngredientRepository.GetAll().Where(p => fids.Contains(p.Fid)).ToArray();
            });
        }

        public async Task<Dictionary<long, IEnumerable<FoodIngredient>>> GetFoodIngredientsDic(long[] fids)
        {
            return await Task.Factory.StartNew(() =>
            {
                fids = (fids ?? new long[0]).Where(p => p > 0).Distinct().ToArray();
                if (fids.Length == 0) return new Dictionary<long, IEnumerable<FoodIngredient>>();
                return _FoodIngredientRepository.GetAll().Where(p => fids.Contains(p.Fid)).ToArray()
                .GroupBy(p=>p.Fid).ToDictionary(p=>p.Key,p=>p.Select(i=>i));
            });
        }
    }
}
