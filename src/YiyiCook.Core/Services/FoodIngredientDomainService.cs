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
        Task<FoodIngredientSource> AddOrUpdateIngredientSource(string name, string unitName);
        Task<IEnumerable<FoodIngredientSource>> SearchIngredientSource(string name, int start, int limit);
        Task AddUpdateAndDeleteFoodIngredients(long fid, AddUpdateAndDeleteFoodIngredientInput[] inputs);
        Task<IEnumerable<FoodIngredientSource>> GetIngredientSourceByIds(long[] ids);
        Task<IEnumerable<FoodIngredient>> GetFoodIngredients(long[] fids);
    }
    public class FoodIngredientDomainService: IFoodIngredientDomainService
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

        public async Task<FoodIngredientSource> AddOrUpdateIngredientSource(string name, string unitName)
        {
            ExceptionHelper.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ExceptionHelper.ThrowIfNullOrWhiteSpace(unitName, nameof(unitName));
            var source = _FoodIngredientSourceRepository.GetAll().Where(p => p.Name == name).FirstOrDefault();
            if (source == null)
                source = new Models.FoodIngredientSource();
            source.Name = name;
            source.UnitName = unitName;
            await _FoodIngredientSourceRepository.InsertOrUpdateAsync(source);
            return source;
        }

        public async Task<IEnumerable<FoodIngredientSource>> SearchIngredientSource(string name, int start, int limit)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _FoodIngredientSourceRepository.GetAll().Where(p => p.Name.Contains(name)).OrderByDescending(p => p.Id).PageBy(start, limit).ToArray();
            });
        }
        public async Task AddUpdateAndDeleteFoodIngredients(long fid, AddUpdateAndDeleteFoodIngredientInput[] inputs)
        {
            ExceptionHelper.ThrowIfNull(inputs, nameof(inputs));
            var updateItems = inputs.Where(p => p.Id > 0); ;
            var ids = updateItems.Select(p => p.Id);
            using (var unitWork = _UnitOfWorkManager.Begin())
            {
                await _FoodIngredientRepository.GetAll().Where(p => p.Fid == fid && !ids.Contains(p.Id)).BatchDeleteAsync();
                var needUpdateItems = _FoodIngredientRepository.GetAll().Where(p => ids.Contains(p.Id)).ToArray();
                foreach (var item in inputs)
                {
                    FoodIngredient ingredient = null;
                    if (item.Id > 0)
                        ingredient = needUpdateItems.Where(p => p.Id == item.Id).FirstOrDefault();
                    if (ingredient == null)
                        ingredient = new FoodIngredient() { Fid = fid, Fiid = item.Fiid };
                    ingredient.Num = item.Num;
                    ingredient.Description = item.Description;
                    await _FoodIngredientRepository.InsertOrUpdateAsync(ingredient);
                }
                unitWork.Complete();
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
                return _FoodIngredientRepository.GetAll().Where(p =>fids.Contains(p.Fid)).ToArray();
            });
        }
    }
}
