using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgnetAbp;
using TgnetAbp.Api;
using TgnetAbp.Data;
using YiyiCook.Core.Input.FoodOrder;
using YiyiCook.Core.IRepositories;
using YiyiCook.Core.Models;
using YiyiCook.Infrastruction.Enum.FoodOrder;

namespace YiyiCook.Core.Services
{
    public interface IFoodOrderDomainService : Abp.Domain.Services.IDomainService
    {
        Task EditFoodOrder(AddFoodOrderInput input);
        Task AddOrUpdateAndDelFoodOrderItem(long foid, AddFoodOrderItemInput[] inputs, bool isDel = true, bool merge = false);
        Task<long> AddFoodOrder(AddFoodOrderInput input, bool merge = false);
        Task<FoodOrder> GetFootOrder(long foid);
        Task<PageModel<FoodOrder>> GetPageFootOrderList(SearchOrderQuery query);

        Task<FoodOrderItem[]> GetFootOrderItems(long foid);
        Task SetOrderState(long foid, FoodOrderState state);
    }
    public class FoodOrderDomainService : IFoodOrderDomainService
    {
        private readonly IUnitOfWorkManager _UnitOfWorkManager;
        private readonly IFoodOrderRepository _FoodOrderRepository;
        private readonly IFoodOrderItemRepository _FoodOrderItemRepository;
        public FoodOrderDomainService(IUnitOfWorkManager unitOfWorkManager,
            IFoodOrderRepository foodOrderRepository,
            IFoodOrderItemRepository foodOrderItemRepository
            )
        {
            _FoodOrderRepository = foodOrderRepository;
            _FoodOrderItemRepository = foodOrderItemRepository;
            _UnitOfWorkManager = unitOfWorkManager;
        }

        public async Task EditFoodOrder(AddFoodOrderInput input)
        {
            ExceptionHelper.ThrowIfNull(input, nameof(input));
            FoodOrder order;
            if (input.Id > 0)
            {
                order = _FoodOrderRepository.FirstOrDefault(input.Id);
                CheckState(order.State);
                order.Date = input.Date;
                order.Type = input.Type;
            }
            else
            {
                order = await _FoodOrderRepository.InsertAsync(new Models.FoodOrder()
                {
                    Date = input.Date,
                    Description = input.Description,
                    State = Infrastruction.Enum.FoodOrder.FoodOrderState.None,
                    Type = input.Type
                });
            }
            _UnitOfWorkManager.Current.SaveChanges();
            await AddOrUpdateAndDelFoodOrderItem(order.Id, input.Items);
        }
        private void CheckState(Infrastruction.Enum.FoodOrder.FoodOrderState state)
        {
            var error = "";
            switch (state)
            {
                case FoodOrderState.Cancel:
                    error = "订单已经取消，不能修改";
                    break;
                case FoodOrderState.Prepairing:
                    error = "订单正在准备中，不能修改";
                    break;
                case FoodOrderState.Done:
                    error = "订单已经完成，不能修改";
                    break;
            }
            if (!string.IsNullOrWhiteSpace(error))
                throw new ExceptionWithErrorCode(ErrorCode.没有操作权限, error);
        }
        public async Task AddOrUpdateAndDelFoodOrderItem(long foid, AddFoodOrderItemInput[] inputs, bool isDel = true, bool merge = false)
        {
            ExceptionHelper.ThrowIfNotId(foid, nameof(foid));
            ExceptionHelper.ThrowIfTrue(inputs == null || !inputs.Any(), "菜品不能为空");
            var fids = inputs.Select(p => p.Fid).ToArray();
            if (isDel)
                await _FoodOrderItemRepository.GetAll().Where(p => p.Foid == foid && !fids.Contains(p.Fid)).BatchDeleteAsync();
            var items = _FoodOrderItemRepository.GetAll().Where(p => p.Foid == foid);
            foreach (var item in inputs)
            {
                var order = items.Where(p => p.Fid == item.Fid).FirstOrDefault();
                if (order == null)
                {
                    await _FoodOrderItemRepository.InsertAsync(new Models.FoodOrderItem()
                    {
                        Foid = foid,
                        Description = item.Description,
                        Num = item.Num,
                        Fid = item.Fid
                    });
                }
                else
                {
                    if (merge)
                        order.Num += item.Num;
                    else
                        order.Num = item.Num;
                }
            }
        }
        public async Task<FoodOrder> GetFootOrder(long foid)
        {
            return await _FoodOrderRepository.GetAsync(foid);
        }
        public async Task<FoodOrderItem[]> GetFootOrderItems(long foid)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _FoodOrderItemRepository.GetAll().Where(p => p.Foid == foid).ToArray();
            });
        }

        public async Task<PageModel<FoodOrder>> GetPageFootOrderList(SearchOrderQuery query)
        {
            return await Task.Factory.StartNew(() =>
            {
                var source = _FoodOrderRepository.GetAll();
                var count = source.Count();
                var orders = source.OrderByDescending(p => p.Date).ThenByDescending(p=>p.LastModificationTime).PageBy(query.Start, query.Limit).ToArray();
                return new PageModel<FoodOrder>(orders, count);
            });
        }

        public async Task SetOrderState(long foid, FoodOrderState state)
        {
            var order = await _FoodOrderRepository.GetAsync(foid);
            var orderState = order.State;
            if (orderState > state)
                throw new ExceptionWithErrorCode(ErrorCode.非法的操作);
            order.State = state;
            await _FoodOrderRepository.UpdateAsync(order);

        }

        public async Task<long> AddFoodOrder(AddFoodOrderInput input, bool merge = false)
        {
            ExceptionHelper.ThrowIfNull(input, nameof(input));
            FoodOrder order;
            if (input.Id > 0)
            {
                order = _FoodOrderRepository.FirstOrDefault(input.Id);
                CheckState(order.State);
                order.Date = input.Date;
                order.Type = input.Type;
            }
            else
            {
                order = await _FoodOrderRepository.InsertAsync(new Models.FoodOrder()
                {
                    Date = input.Date,
                    Description = input.Description,
                    State = Infrastruction.Enum.FoodOrder.FoodOrderState.None,
                    Type = input.Type
                });
            }
            _UnitOfWorkManager.Current.SaveChanges();
            await AddOrUpdateAndDelFoodOrderItem(order.Id, input.Items,false, merge);
            return order.Id;
        }

    }
}
