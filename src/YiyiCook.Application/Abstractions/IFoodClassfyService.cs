using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YiyiCook.Application.Dto.FoodClassfy;

namespace YiyiCook.Application.Abstractions
{
    public interface IFoodClassfyService:Abp.Application.Services.IApplicationService
    {
        Task<IEnumerable<FoodClassfyListItemDto>> GetAllClassfy();
    }
}
