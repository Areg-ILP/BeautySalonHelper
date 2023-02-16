using BeautySalonService.BusinessLayer.Enums;
using BeautySalonService.BusinessLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeautySalonService.BusinessLayer.ActionFIlter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ClientAuthorizationAttribute : ActionFilterAttribute
    {
        private readonly RoleTypes[] _accessibleRoles;

        public ClientAuthorizationAttribute(params RoleTypes[] roles)
        {
            _accessibleRoles = roles;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (ClientHelper.IsAuthorized)
            {
                var clientDetails = ClientHelper.ClientDetails;
                if (_accessibleRoles.Contains(RoleTypes.All) ||
                    _accessibleRoles.Contains((RoleTypes) clientDetails.Role.Id))
                {
                    await next.Invoke();
                }
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
