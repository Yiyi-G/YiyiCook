using System.Threading.Tasks;
using YiyiCook.Web.Controllers;
using Shouldly;
using Xunit;

namespace YiyiCook.Web.Tests.Controllers
{
    public class HomeController_Tests: YiyiCookWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
