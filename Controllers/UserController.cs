using BeautySalonService.BusinessLayer.ActionFIlter;
using BeautySalonService.BusinessLayer.Enums;
using BeautySalonService.BusinessLayer.Helpers;
using BeautySalonService.DataLayer.Context;
using BeautySalonService.Models.Identity;
using BeautySalonService.ViewModels.Admin.Role;
using BeautySalonService.ViewModels.Admin.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautySalonService.Controllers
{
    [Route("Admin/{Controller}/{Action=Index}")]
    [ClientAuthorization(RoleTypes.SuperAdmin)]
    public class UserController : Controller
    {
        private readonly BeautySalonServiceDbContext _context;

        public UserController(BeautySalonServiceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = new AdminUserIndexViewModel
            {
                ClientsDetails = await _context.Clients
                .Include(x => x.Role)
                .Select(c => new ClientDetialsModel
                {
                    ClientId = c.Id,
                    Email = c.Email,
                    Name = c.Name,
                    MobileNumber = c.MobileNumber,
                    SureName = c.SureName,
                    Role = new RoleDetailsModel
                    {
                        Id = c.RoleId,
                        Name = c.Role.Name,
                    },
                }).ToListAsync(),
                UseAdminCss = true,
                UseFooter = false,
                UseHeader = false,
                UseGlobalCss = false,
            };

            return View("Users", users);
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(int userId)
        {
            var userDetails = await _context.Clients.Include(c => c.Role).SingleOrDefaultAsync(c => c.Id == userId);
            if (userDetails == null)
            {
                return NotFound("User not found.");
            }

            var userDetailsForAdmin = new UserDetailsForAdmin
            {
                Id = userDetails.Id,
                Name = userDetails.Name,
                Surname = userDetails.SureName,
                Role = userDetails.Role.Name,
                Email = userDetails.Email,
                MobileNumber = userDetails.MobileNumber,
                CreationDate = userDetails.CreationDate,
                BooksCount = 5,
            };

            return Json(userDetailsForAdmin);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserRole(int userId, int roleId)
        {
            var userDetails = await _context.Clients.SingleOrDefaultAsync(c => c.Id == userId);
            if (userDetails == null)
            {
                return NotFound("User not found.");
            }
            else if (roleId == userDetails.RoleId)
            {
                return BadRequest("User role already that role which you want to update.");
            }

            var roleDetails = await _context.Roles.SingleOrDefaultAsync(r => r.Id == roleId);
            if (roleDetails == null)
            {
                return NotFound("Role not found.");
            }

            try
            {
                userDetails.RoleId = roleId;
                userDetails.Role = roleDetails;

                _context.Update(userDetails);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Updateing user role error.\nException message: {ex.Message}");
            }

            if (ClientHelper.ClientDetails.ClientId == userDetails.Id)
            {
                ClientHelper.ClientDetails.Role.Id = roleId;
                ClientHelper.ClientDetails.Role.Name = roleDetails.Name;
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var userDetails = await _context.Clients.SingleOrDefaultAsync(c => c.Id == userId);
            if (userDetails == null)
            {
                return NotFound("User not found.");
            }

            try
            {
                _context.Remove(userDetails);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Deleting client error.\nException message: {ex.Message}");
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            if (roles == null)
            {
                return NotFound("User not found.");
            }

            var rolesDetails = roles.Select(r => new RoleDetailsForAdmin
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return Json(rolesDetails);
        }
    }
}
