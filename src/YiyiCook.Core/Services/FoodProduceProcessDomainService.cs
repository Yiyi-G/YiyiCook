using Abp.Domain.Uow;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgnetAbp;
using YiyiCook.Core.Input.FoodProduceProcess;
using YiyiCook.Core.IRepositories;
using YiyiCook.Core.Models;

namespace YiyiCook.Core.Services
{
    public interface IFoodProduceProcessDomainService : Abp.Domain.Services.IDomainService
    {
        Task AddUpdateAndDeleteFoodProduceProcess(long fid, AddUpdateAndDeleteFoodProduceProcessInput[] inputs);
    }
    public class FoodProduceProcessDomainService: IFoodProduceProcessDomainService
    {
        private readonly IUnitOfWorkManager _UnitOfWorkManager;
        private readonly IFoodProduceProcessRepository _FoodProduceProcessRepository;
        private readonly IFoodProduceProcessImgRepository _FoodProduceProcessImgRepository;

        public FoodProduceProcessDomainService(IUnitOfWorkManager unitOfWorkManager,
            IFoodProduceProcessRepository foodProduceProcessRepository,
            IFoodProduceProcessImgRepository foodProduceProcessImgRepository
            )
        {
            _UnitOfWorkManager = unitOfWorkManager;
            _FoodProduceProcessRepository = foodProduceProcessRepository;
            _FoodProduceProcessImgRepository = foodProduceProcessImgRepository;
        }
        public async Task AddUpdateAndDeleteFoodProduceProcess(long fid, AddUpdateAndDeleteFoodProduceProcessInput[] inputs)
        {
            ExceptionHelper.ThrowIfNull(inputs, nameof(inputs));
            var updateItems = inputs.Where(p => p.Id > 0); ;
            var ids = updateItems.Select(p => p.Id);
            using (var unitWork = _UnitOfWorkManager.Begin())
            {
                await _FoodProduceProcessRepository.GetAll().Where(p => p.Fid == fid && !ids.Contains(p.Id)).BatchUpdateAsync(new FoodProduceProcess() { IsEnabled=false});
                var needUpdateItems = _FoodProduceProcessRepository.GetAll().Where(p => ids.Contains(p.Id)).ToArray();
                foreach (var item in inputs)
                {
                    FoodProduceProcess process = null;
                    if (item.Id > 0)
                        process = needUpdateItems.Where(p => p.Id == item.Id).FirstOrDefault();
                    if (process == null)
                        process = new FoodProduceProcess() { Fid = fid};
                    process.RankNum = item.RankNum;
                    process.Description = item.Description;
                    await _FoodProduceProcessRepository.InsertOrUpdateAsync(process);
                    await AddOrUpdateFoodProduceProcessImgs(process.Id, item.Imags);
                }
                unitWork.Complete();
            }
        }
        private async Task AddOrUpdateFoodProduceProcessImgs(long fppid, string[] imagUrls)
        {
            ExceptionHelper.ThrowIfNotId(fppid, nameof(fppid));
            if (imagUrls != null)
            {
                if (imagUrls.Any())
                {
                    _FoodProduceProcessImgRepository.GetAll().Where(p => p.Fppid == fppid && !imagUrls.Contains(p.Url)).BatchDelete();
                    var imgs = _FoodProduceProcessImgRepository.GetAll().Where(p => p.Fppid == fppid).ToArray();
                    var addImgs = imagUrls.Where(p => !imgs.Any(i => i.Url == p));
                    foreach (var item in addImgs)
                    {
                        await _FoodProduceProcessImgRepository.InsertAsync(new FoodProduceProcessImg()
                        {
                            Fppid = fppid,
                            Url = item,
                        });

                    }
                }
                else
                {
                    _FoodProduceProcessImgRepository.GetAll().Where(p => p.Fppid == fppid).BatchDelete();
                }
            }

        }


    }
}
