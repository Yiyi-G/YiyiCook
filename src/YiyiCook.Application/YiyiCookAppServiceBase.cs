using Abp.Application.Services;
using YiyiCook.Core;

namespace YiyiCook.Application
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class YiyiCookAppServiceBase : ApplicationService
    {
        protected YiyiCookAppServiceBase()
        {
            LocalizationSourceName = YiyiCookConsts.LocalizationSourceName;
        }
    }
}