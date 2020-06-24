using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YiyiCook.Application.Dto.FoodProduceProcess;

namespace YiyiCook.Application.Abstractions
{
    public interface IFoodProduceProcessService : Abp.Application.Services.IApplicationService
    {
        Task AddUpdateAndDeleteFoodProduceProcess(AddUpdateAndDeleteFoodProduceProcessesInputDto input);
    }
}
