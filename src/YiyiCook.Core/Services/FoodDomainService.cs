using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgnetAbp;
using TgnetAbp.Data;
using YiyiCook.Core.Input.Food;
using YiyiCook.Core.IRepositories;
using YiyiCook.Core.Models;

namespace YiyiCook.Core.Services
{
    public interface IFoodDomainService : Abp.Domain.Services.IDomainService
    {
        Task<PageModel<Models.Food>> Search(FoodSearchQuery query);
        Task<Models.Food> Get(long id);
        Task<IEnumerable<Models.Food>> Get(long[] id);
        Task<IEnumerable<Models.FoodImg>> GetFoodImgs(long fid);
        Task<IEnumerable<Models.FoodImg>> GetFoodImgs(long[] fids);
        Task AddOrUpdateFood(AddOrUpdateFoodInput input);
    }
    public class FoodDomainService : IFoodDomainService
    {
        private readonly IFoodRepository _FoodRepository;
        private readonly IFoodImgRepository _FoodImgRepository;

        public FoodDomainService(
            IFoodRepository foodRepository,
            IFoodImgRepository foodImgRepository
            )
        {
            _FoodRepository = foodRepository;
            _FoodImgRepository = foodImgRepository;

        }

        public async Task<PageModel<Models.Food>> Search(FoodSearchQuery query)
        {
            return await Task.Factory.StartNew(() =>
            {
                ExceptionHelper.ThrowIfNull(query, nameof(query));
                var source = _FoodRepository.GetAll().Where(p => p.IsEnabled);
                if (!string.IsNullOrWhiteSpace(query.kw))
                    source = source.Where(p => p.Name.Contains(query.kw));
                if (query.fcid.HasValue)
                    source = source.Where(p => p.Fcid == query.fcid.Value);
                var count = source.Count();
                var model = source.OrderByDescending(p => p.CreationTime).PageBy(query.start, query.limit).ToArray();
                return new PageModel<Food>(model,count);
            });
        }
        public async Task<Models.Food> Get(long id)
        {
            return await _FoodRepository.GetAsync(id);
        }
        public async Task<IEnumerable<Models.Food>> Get(long[] ids)
        {
            return await Task.Factory.StartNew(() =>
            {
                ids = (ids ?? new long[0]).Where(p => p > 0).Distinct().ToArray();
                if (ids.Length == 0) return new Food[0];
                return _FoodRepository.GetAll().Where(p => ids.Contains(p.Id)).ToArray();
            });
        }

        public async Task<IEnumerable<Models.FoodImg>> GetFoodImgs(long fid)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _FoodImgRepository.GetAll().Where(p => p.Fid == fid).ToArray();
            });
        }
        public async Task<IEnumerable<Models.FoodImg>> GetFoodImgs(long[] fids)
        {
            return await Task.Factory.StartNew(() =>
            {
                fids = (fids ?? new long[0]).Where(p => p > 0).Distinct().ToArray();
                if (fids.Length == 0) return new FoodImg[0];
                return _FoodImgRepository.GetAll().Where(p => fids.Contains(p.Fid)).ToArray();
            });
        }

        public async Task AddOrUpdateFood(AddOrUpdateFoodInput input)
        {
            Food food = null;
            bool isAdd = false;
            if (input.Id > 0)
                food = await _FoodRepository.GetAsync(input.Id);
            if (food == null)
            {
                food = new Food();
            }
            if (input.Fcid.HasValue)
                food.Fcid = input.Fcid;
            if (!string.IsNullOrWhiteSpace(input.Name))
                food.Name = input.Name;
            if (!string.IsNullOrWhiteSpace(input.Description))
                food.Description = input.Description;
            if (!string.IsNullOrWhiteSpace(input.HeadImgUrl))
                food.HeadImgUrl = input.HeadImgUrl;
            if (!string.IsNullOrWhiteSpace(input.VideoUrl))
                food.VideoUrl = input.VideoUrl;
            if (!string.IsNullOrWhiteSpace(input.ProduceVideoUrl))
                food.ProduceVideoUrl = input.ProduceVideoUrl;
                await _FoodRepository.InsertOrUpdateAsync(food);
        }

        public async Task AddOrUpdateFoodImgs(long fid, string[] imagUrls)
        {
            ExceptionHelper.ThrowIfNotId(fid,nameof(fid));
            if (imagUrls != null)
            {
                if (imagUrls.Any())
                {
                    _FoodImgRepository.GetAll().Where(p => p.Fid == fid && !imagUrls.Contains(p.Url)).BatchDelete();
                    var imgs = _FoodImgRepository.GetAll().Where(p => p.Fid == fid).ToArray();
                    var addImgs = imagUrls.Where(p => !imgs.Any(i => i.Url == p));
                    foreach (var item in addImgs)
                    {
                        await _FoodImgRepository.InsertAsync(new FoodImg()
                        {
                            Fid = fid,
                            Url = item
                        });

                    }
                }
                else 
                {
                    _FoodImgRepository.GetAll().Where(p => p.Fid == fid).BatchDelete();
                }
            }

        }

    }
}
