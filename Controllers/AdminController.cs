using BeautySalonService.DataLayer.Context;
using BeautySalonService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BeautySalonService.Models.Identity;

namespace BeautySalonService.Controllers
{
    public class AdminController : Controller
    {
        private readonly BeautySalonServiceDbContext _context;
        public AdminController(BeautySalonServiceDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new AdminViewModel
            {
                ClientsDetails = _context.Clients.Select(c => new ClientDetialsModel
                {
                    ClientId = c.Id,
                    Email = c.Email,
                    Name= c.Name,
                    Role = new RoleDetailsModel
                    {
                        Id= c.Id,
                        Name = c.Name,
                    },
                    SureName = c.SureName
                }).ToList(),
                UseFooter = true,
                UseHeader = false,
            }); ;
        }
    }
}
