using GameAuditor.Controllers;
using GameAuditor.Models;
using GameAuditor.Repositories.Implimentations;
using GameAuditor.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GameAuditor.Services.UserService
{
    public class OwnerAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, Post>
    {
        UserManager<IdentityUser> _userManager;

        public OwnerAuthorizationHandler(UserManager<IdentityUser>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Post resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for CRUD permission, return.

            if (requirement.Name != EntityRepository.Update &&
                requirement.Name != IEntityRepository.Delete)

            {
                return Task.CompletedTask;
            }

            if (resource.OwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
