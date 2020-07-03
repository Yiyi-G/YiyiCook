using Microsoft.AspNetCore.Mvc;
using TgnetAbp.Api;

namespace YiyiCook.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HomeController : YiyiCookControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.JsonApiResult(ErrorCode.None);
        }
        [HttpPost]
        public IActionResult About()
        {
            return this.JsonApiResult(ErrorCode.None);
        }
    }
}