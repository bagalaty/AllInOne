using Microsoft.AspNetCore.Authorization;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllInOne.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class BackEndAuthorizationHandler : AuthorizationHandler<BackEndAuthRequirement>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BackEndAuthorizationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BackEndAuthRequirement requirement)
        {
            //if (AppController.CurrentUser == null && !AppController.TryAuthenticate(_unitOfWork))
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BackEndAuthRequirement : IAuthorizationRequirement { }
}
