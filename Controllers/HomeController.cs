using BeautySalonService.Models;
using BeautySalonService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BeautySalonService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View(new HomeViewModel());
        }

        public IActionResult Error(string code, string message)
        {
            return View(new ErrorViewModel()
            {
                UseFooter = false,
                UseHeader = false,
                UseGlobalCss = false,
                UseErrorCss = true,
                Code = code,
                Message = message,
            });
        }
    }
}
