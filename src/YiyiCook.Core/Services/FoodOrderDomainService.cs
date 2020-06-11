using Abp.Domain.Uow;
using Abp.Linq.Extensions;
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
        Task AddFoodOrder(AddFoodOrderInput input);
        Task AddFoodOrderItem(long foid, AddFoodOrderItemInput[] inputs);
        Task<FoodOrder> GetFootOrder(long foid);
        Task<PageModel<FoodOrder>> GetPageFootOrderList(SearchOrderQuery query);

        Task<FoodOrderItem[]> GetFootOrderItems(long foid);
        Task SetOrderState(long foid, FoodOrderState state);
    }
    public class FoodOrderDomainService: IFoodOrderDomainService
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

        public async Task AddFoodOrder(AddFoodOrderInput input)
        {
            ExceptionHelper.ThrowIfNull(input, nameof(input));
            using (var unitWork = _UnitOfWorkManager.Begin())
            {
                var order = await _FoodOrderRepository.InsertAsync(new Models.FoodOrder()
                {
                    Date = input.Date,
                    Description = input.Description,
                    State = Infrastruction.Enum.FoodOrder.FoodOrderState.None,
                    Type = input.Type
                });
                await AddFoodOrderItem(order.Id, input.Items);
                unitWork.Complete();
            }
        }
        public async Task AddFoodOrderItem(long foid, AddFoodOrderItemInput[] inputs)
        {
            ExceptionHelper.ThrowIfNotId(foid, nameof(foid));
            ExceptionHelper.ThrowIfTrue(inputs == null || !inputs.Any(), "菜品不能为空");
            foreach (var item in inputs)
            {
                await _FoodOrderItemRepository.InsertAsync(new Models.FoodOrderItem()
                {
                    Foid = foid,
                    Description = item.Description,
                    Num = item.Num,
                    Fid = item.Fid
                });
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
                var orders = source.OrderByDescending(p=>p.LastModificationTime).PageBy(query.Start,query.Limit).ToArray();
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
    }
}
