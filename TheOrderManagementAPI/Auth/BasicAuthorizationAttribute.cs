using Microsoft.AspNetCore.Authorization;

namespace TheOrderManagementAPI.Auth
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
}
