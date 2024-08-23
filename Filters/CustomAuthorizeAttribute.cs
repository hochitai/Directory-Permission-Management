using DirectoryPermissionManagement.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DirectoryPermissionManagement.Filters
{
    public sealed class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Add information of user for controller
            var identity = user.Identity as ClaimsIdentity;
            var userId = TokenHelper.GetUserIdFromToken(identity);

            context.HttpContext.Items["userId"] = userId;
        }
    }
}
