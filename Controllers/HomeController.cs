using BeautySalonService.Models.Home;
using BeautySalonService.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View(new HomeViewModel());
        }

        public IActionResult Error(ErrorDetailsModel request)
        {
            return View(new ErrorViewModel()
            {
                UseFooter = false,
                UseHeader = false,
                UseGlobalCss = false,
                UseErrorCss = true,
                Code = request.Code,
                Message = request.Message,
            });
        }
    }
}
