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
    }
}
