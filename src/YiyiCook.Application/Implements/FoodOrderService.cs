using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgnetAbp.Data;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto.Food;
using YiyiCook.Application.Dto.FoodOrder;
using YiyiCook.Application.Dto.FoodOrder.Input;
using YiyiCook.Core.Input.FoodOrder;
using YiyiCook.Core.Services;

namespace YiyiCook.Application.Implements
{
    public class FoodOrderService : IFoodOrderService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IFoodOrderDomainService _FoodOrderDomainService;
        private readonly IFoodDomainService _FoodDomainService;
        public FoodOrderService(
            IObjectMapper objectMapper,
            IFoodOrderDomainService foodOrderDomainService,
            IFoodDomainService foodDomainService)
        {
            _objectMapper = objectMapper;
            _FoodOrderDomainService = foodOrderDomainService;
            _FoodDomainService = foodDomainService;
        }

        public async Task EditFoodOrder(AddFoodOrderInputDto input)
        {
            await _FoodOrderDomainService.EditFoodOrder(_objectMapper.Map<AddFoodOrderInput>(input));
        }
        public async Task<long> AddFoodOrder(AddFoodOrderInputDto input)
        {
            return await _FoodOrderDomainService.AddFoodOrder(_objectMapper.Map<AddFoodOrderInput>(input),true);
        }
        public async Task AddOrUpdateAndDelFoodOrderItem(AddFoodOrderItemsInputDto input)
        {
            await _FoodOrderDomainService.AddOrUpdateAndDelFoodOrderItem(input.Foid, _objectMapper.Map<AddFoodOrderItemInput[]>(input.Items));
        }
        public async Task<FoodOrderDto> GetFootOrder(long foid)
        {
            var order = await _FoodOrderDomainService.GetFootOrder(foid);
            var orderItems = await _FoodOrderDomainService.GetFootOrderItems(foid);
            var fids = orderItems.Select(p => p.Fid).ToArray();
            var foods = await _FoodDomainService.Get(fids);
            var foodImgs = await _FoodDomainService.GetFoodImgs(fids);
            var orderDto = _objectMapper.Map<FoodOrderDto>(order);
            var orderItemDtoList = new List<FoodOrderItemDto>();
            foreach (var item in orderItems)
            {
                var orderItemDto = _objectMapper.Map<FoodOrderItemDto>(item);
                var food = foods.FirstOrDefault(p => p.Id == item.Fid);
                orderItemDto.Food = _objectMapper.Map<FoodDto>(food);
                orderItemDto.Food.ImgIds = foodImgs.Where(p => p.Fid == item.Fid).Select(p => p.FileId).ToArray();
                orderItemDtoList.Add(orderItemDto);
            }
            orderDto.Item = orderItemDtoList.ToArray();
            return orderDto;
        }
        public async Task<PageModel<FoodOrderDto>> GetPageFootOrderList(SearchOrderQueryDto query)
        {
            var model = await _FoodOrderDomainService.GetPageFootOrderList(_objectMapper.Map<SearchOrderQuery>(query));
            var orders = model.Models.Select(p => _objectMapper.Map<FoodOrderDto>(p)).ToArray();
            return new PageModel<FoodOrderDto>() {Models = orders,Count = model.Count };
        }
        public async Task SetOrderState(UpdateOrderStateInputDto input)
        {
            await _FoodOrderDomainService.SetOrderState(input.Foid, input.State);
        }

      
    }
}
