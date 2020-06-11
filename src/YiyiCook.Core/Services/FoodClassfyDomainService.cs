using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgnetAbp;
using YiyiCook.Core.IRepositories;
using YiyiCook.Core.Models;

namespace YiyiCook.Core.Services
{
    public interface IFoodClassfyDomainService : Abp.Domain.Services.IDomainService
    {
        Task<IEnumerable<FoodClassfy>> GetAllClassfies();
        Task<IEnumerable<FoodClassfy>> GetClassfiesByIds(long[] ids);
        Task<FoodClassfy> GetOrCreate(string name);
    }
    public class FoodClassfyDomainService: IFoodClassfyDomainService
    {
        private readonly IFoodClassfyRepository _FoodClassfyRepository;
        public FoodClassfyDomainService(IFoodClassfyRepository foodClassfyRepository)
        {
            _FoodClassfyRepository = foodClassfyRepository;
        }

        public async Task<IEnumerable<FoodClassfy>> GetAllClassfies()
        {
            return await Task.Factory.StartNew(() =>
            {
                return _FoodClassfyRepository.GetAll().ToArray();
            });
        }
        public async Task<IEnumerable<FoodClassfy>> GetClassfiesByIds(long[] ids)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _FoodClassfyRepository.GetAll().Where(p=>ids.Contains(p.Id)).ToArray();
            });
        }

        public async Task<FoodClassfy> GetOrCreate(string name)
        {
            ExceptionHelper.ThrowIfNullOrWhiteSpace(name, nameof(name));
            var classfy = _FoodClassfyRepository.GetAll().Where(p => p.Name == name&&p.IsEnabled).FirstOrDefault();
            if (classfy == null)
            {
              classfy = await  _FoodClassfyRepository.InsertAsync(new FoodClassfy() { Name = name });
            }
            return classfy;
        }
    }
}
