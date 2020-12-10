using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CopBookApi.Handlers.Authorization
{

    public class CanEditRequestedResourceHandler : IAuthorizationHandler
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CanEditRequestedResourceHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            string userId = context.User.Claims.First((claim) => claim.Type == "id").Value;
            var resourceId = _httpContextAccessor.HttpContext.Request.RouteValues;
            return Task.CompletedTask;
        }
    }
}