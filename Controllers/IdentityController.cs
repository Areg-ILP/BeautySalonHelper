using BeautySalonService.BusinessLayer.ActionFIlter;
using BeautySalonService.BusinessLayer.Enums;
using BeautySalonService.BusinessLayer.Helpers;
using BeautySalonService.DataLayer.Context;
using BeautySalonService.DataLayer.Entities;
using BeautySalonService.Models.Identity;
using BeautySalonService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeautySalonService.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class IdentityController : Controller
    {
        private readonly BeautySalonServiceDbContext _context;
        public IdentityController(BeautySalonServiceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new SignInViewModel
            {
                UseHeader = false,
                UseFooter = false,
                UseGlobalCss = false,
                UseIdentityCss = true,
            });
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new SignUpViewModel
            {
                UseHeader = false,
                UseFooter = false,
                UseGlobalCss = false,
                UseIdentityCss = true,
            });
        }

		[HttpGet]
		[ClientAuthorization(RoleTypes.All)]
		public IActionResult ResetPassword()
		{
			return View(new ResetPasswordViewModel
			{
				UseHeader = false,
				UseFooter = false,
				UseGlobalCss = false,
				UseIdentityCss = true,
			});
		}

		[HttpGet]
        [ClientAuthorization(RoleTypes.All)]
        public IActionResult Profile(int clientId)
        {
            if (ClientHelper.ClientDetails.ClientId == clientId)
            {
                var client = _context.Clients.Include(x => x.Role).Single(x => x.Id == clientId);
                return View(new ProfileViewModel
                {
                    Id = client.Id,
                    Email = client.Email,
                    Name = client.Name,
                    SureName = client.SureName,
                    MobileNumber= client.MobileNumber,
                    RoleName = client.Role.Name,
                    UseFooter = false,
                    UseHeader = false,
                    UseGlobalCss = false,
                    UseIdentityCss = true,
                });
            }
            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        [ClientAuthorization(RoleTypes.All)]
        public IActionResult SignOut(int clientId)
        {
            ClientHelper.UnAuthorize(clientId);
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        [ClientAuthorization(RoleTypes.All)]
        public async Task<IActionResult> ResetPassword([Required] ResetPasswordModel request)
        {
            if (ModelState.IsValid
                && request.IsPasswordEqualConfirmPassword
                && ClientHelper.ClientDetails.ClientId == request.Id
                )
            {
                try
                {
                    var clientToUpdate = _context.Clients.Single(c => c.Id == request.Id);
                    if (clientToUpdate != null)
                    {
                        string oldPassHash = HashHelper.GetSoltedHash(request.OldPassword, clientToUpdate.Email);
                        if (oldPassHash == clientToUpdate.Password)
                        {
                            clientToUpdate.Password = HashHelper.GetSoltedHash(request.NewPassword, clientToUpdate.Email);

                            _context.Update(clientToUpdate);
                            await _context.SaveChangesAsync();

                            return RedirectToAction("Profile", "Identity", new { request.Id });
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(
                            "Add",
                            $"Error Message: Updateing client error.\nException message: {ex.Message}"
                        );
                }
            }

            return ValidationProblem(); // MB Error Page
        }

        [HttpPost]
        [ClientAuthorization(RoleTypes.All)]
        public async Task<IActionResult> EditProfile([Required] EditProfileModel request)
        {
            if (ModelState.IsValid && ClientHelper.ClientDetails.ClientId == request.Id)
            {
                try
                {
                    throw new Exception();
                    var clientToUpdate = _context.Clients.Single(c => c.Id == request.Id);
                    if (clientToUpdate != null)
                    {
                        clientToUpdate.Name = request.Name;
                        clientToUpdate.SureName = request.SureName;
                        clientToUpdate.Email = request.Email;
                        clientToUpdate.MobileNumber = request.MobileNumber;

                        _context.Update(clientToUpdate);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home",
                    new
                    {
                        Code = "500",
                        Message = $"Updateing client error.\nException message: {ex.Message}"
                    });
                }
            }

            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public IActionResult Login([Required] LoginModel request)
        {
            if (ModelState.IsValid)
            {
                string passHash = HashHelper.GetSoltedHash(request.Password, request.Email);
                var client = _context.Clients.Include(x => x.Role)
                                             .Single(x => x.Email == request.Email
                                                       && x.Password == passHash);

                if (client != null)
                {
                    ClientHelper.Authorize(new ClientDetialsModel
                    {
                        ClientId = client.Id,
                        Email = client.Email,
                        Name = client.Name,
                        SureName = client.SureName,
                        MobileNumber = client.MobileNumber,
                        Role = new RoleDetailsModel
                        {
                            Id = client.RoleId,
                            Name = client.Role.Name
                        }
                    });

                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    return ValidationProblem(detail: "Not activated client."); //MB Error Page
                }
            }

            return RedirectToAction("SignIn");
        }

        [HttpPost]
        public async Task<IActionResult> Register([Required] RegistrationModel request)
        {
            if (ModelState.IsValid && request.IsPasswordEqualConfirmPassword)
            {
                bool busyEmail = _context.Clients.Any(c => c.Email == request.Email);
                if (!busyEmail)
                {
                    string passHash = HashHelper.GetSoltedHash(request.Password, request.Email);
                    try
                    {
                        var newClient = new Client
                        {
                            Email = request.Email,
                            Password = passHash,
                            Name = request.Name,
                            SureName = request.SureName,
                            MobileNumber = request.MobileNumber,
                            RoleId = 1,
                        };

                        await _context.AddAsync(newClient);
                        await _context.SaveChangesAsync();

                        ClientHelper.Authorize(new ClientDetialsModel
                        {
                            ClientId = newClient.Id,
                            Email = newClient.Email,
                            Name = newClient.Name,
                            SureName = newClient.SureName, 
                            MobileNumber= newClient.MobileNumber,
                            Role = new RoleDetailsModel
                            {
                                Id = newClient.RoleId,
                                Name = _context.Roles.Single(x => x.Id == newClient.RoleId)?.Name
                            }
                        });

                        return RedirectToAction("Home", "Home");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(
                            "Add",
                            $"Error Message: Inserting client error.\nException message: {ex.Message}"
                        );
                    }
                }
            } 
            
            return ValidationProblem(); //MB Error Page
        }
    }
}
