using BeautySalonService.BusinessLayer.ActionFIlter;
using BeautySalonService.BusinessLayer.Enums;
using BeautySalonService.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BeautySalonService.Controllers
{
    /// <summary>
    /// NEED HTML AND ADD/GET/DETAILS/DELETE METHODS !!!!!
    /// </summary>
    [Route("Admin/{Controller}/{Action=Index}")]
    [ClientAuthorization(RoleTypes.SuperAdmin)]
    public class RoleController : Controller
    {
        private readonly BeautySalonServiceDbContext _context;

        public RoleController(BeautySalonServiceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }
    }
}
