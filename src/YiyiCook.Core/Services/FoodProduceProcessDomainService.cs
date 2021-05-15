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
        Task<Models.FoodProduceProcess[]> GetFoodProduceProcess(long fid);
        Task<Models.FoodProduceProcessImg[]> GetFoodProduceProcessImgs(long[] fppids);
    }
    public class FoodProduceProcessDomainService : IFoodProduceProcessDomainService
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
            await _FoodProduceProcessRepository.GetAll().Where(p => p.Fid == fid && !ids.Contains(p.Id)).BatchUpdateAsync(new FoodProduceProcess() {  IsEnabled=false }, new string[] { "IsEnabled" }.ToList());
            var needUpdateItems = _FoodProduceProcessRepository.GetAll().Where(p => ids.Contains(p.Id)).ToArray();
            foreach (var item in inputs)
            {
                FoodProduceProcess process = null;
                if (item.Id > 0)
                    process = needUpdateItems.Where(p => p.Id == item.Id).FirstOrDefault();
                if (process == null)
                    process = new FoodProduceProcess() { Fid = fid,IsEnabled=true };
                process.RankNum = item.RankNum;
                process.Description = item.Description;
                await _FoodProduceProcessRepository.InsertOrUpdateAsync(process);
                _UnitOfWorkManager.Current.SaveChanges();
                await AddOrUpdateFoodProduceProcessImgs(process.Id, item.ImgIds);
            }
        }
        private async Task AddOrUpdateFoodProduceProcessImgs(long fppid, long[] ImgIds)
        {
            ExceptionHelper.ThrowIfNotId(fppid, nameof(fppid));
            ImgIds = ImgIds.Where(p => p > 0).ToArray();
            if (ImgIds.Any())
            {
                _FoodProduceProcessImgRepository.GetAll().Where(p => p.Fppid == fppid && !ImgIds.Contains(p.FileId)).BatchDelete();
                var imgs = _FoodProduceProcessImgRepository.GetAll().Where(p => p.Fppid == fppid).ToArray();
                var addImgs = ImgIds.Where(p => !imgs.Any(i => i.FileId == p));
                foreach (var item in addImgs)
                {
                    await _FoodProduceProcessImgRepository.InsertAsync(new FoodProduceProcessImg()
                    {
                        Fppid = fppid,
                        FileId = item,
                        IsEnabled = true,
                    });

                }
            }
            else
            {
                _FoodProduceProcessImgRepository.GetAll().Where(p => p.Fppid == fppid).BatchDelete();
            }

        }
        public async Task<Models.FoodProduceProcess[]> GetFoodProduceProcess(long fid)
        {
            ExceptionHelper.ThrowIfNotId(fid, nameof(fid));
            return await Task.Factory.StartNew(() =>
            {
                return _FoodProduceProcessRepository.GetAll().Where(p => p.Fid == fid&&p.IsEnabled==true).ToArray();
            });
        }
        public async Task<Models.FoodProduceProcessImg[]> GetFoodProduceProcessImgs(long[] fppids)
        {
            fppids = fppids.Where(p => p > 0).Distinct().ToArray(); ;
            return await Task.Factory.StartNew(() =>
            {
                return _FoodProduceProcessImgRepository.GetAll().Where(p => fppids.Contains(p.Fppid)&& p.IsEnabled == true).ToArray();
            });
        }


    }
}
