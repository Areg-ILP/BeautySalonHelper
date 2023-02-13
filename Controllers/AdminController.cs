using BeautySalonService.DataLayer.Context;
using BeautySalonService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BeautySalonService.Models.Identity;
using BeautySalonService.BusinessLayer.ActionFIlter;
using BeautySalonService.BusinessLayer.Enums;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonService.Controllers
{
    public class AdminController : Controller
    {
        private readonly BeautySalonServiceDbContext _context;
        public AdminController(BeautySalonServiceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ClientAuthorization(RoleTypes.All)]
        public IActionResult Users()
        {
            return View(new AdminViewModel
            {
                ClientsDetails = _context.Clients
                .Include(x => x.Role)
                .Select(c => new ClientDetialsModel
                {
                    ClientId = c.Id,
                    Email = c.Email,
                    Name= c.Name,
                    MobileNumber = c.MobileNumber,
                    SureName = c.SureName,
                    Role = new RoleDetailsModel
                    {
                        Id= c.RoleId,
                        Name = c.Role.Name,
                    },
                }).ToList(),
                UseAdminCss = true,
                UseFooter = false,
                UseHeader = false,
            });
        }


        [HttpGet]
        [ClientAuthorization(RoleTypes.All)]
        public IActionResult UserDetails(int userId)
        {
            var userDetails = _context.Clients.Include(c => c.Role).SingleOrDefault(c => c.Id == userId);
            // havaqi model nor veradarcru vor chkaxi. hamel siruna
            if (userDetails == null)
            {
                return RedirectToAction("Error", "Home",
                new
                {
                    Code = 404.ToString(),
                    Message = $"Not Found.\nInvalid client Id."
                });
            }
            
            return Json(userDetails);
        }
    }
}
