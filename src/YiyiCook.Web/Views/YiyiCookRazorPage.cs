using Abp.AspNetCore.Mvc.Views;
using YiyiCook.Core;

namespace YiyiCook.Web.Views
{
    public abstract class YiyiCookRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected YiyiCookRazorPage()
        {
            LocalizationSourceName = YiyiCookConsts.LocalizationSourceName;
        }
    }
}
