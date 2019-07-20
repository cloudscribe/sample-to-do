using Microsoft.AspNetCore.Authorization;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Authorization
    {
        public static AuthorizationOptions SetupAuthorizationPolicies(this AuthorizationOptions options)
        {
            //https://docs.asp.net/en/latest/security/authorization/policies.html

            options.AddCloudscribeCoreDefaultPolicies();
            options.AddCloudscribeLoggingDefaultPolicy();


            options.AddPolicy(
                "FileManagerPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });

            options.AddPolicy(
               "FileUploadPolicy",
               authBuilder =>
               {
                   authBuilder.RequireAuthenticatedUser();
               });

            options.AddPolicy(
                "FileManagerDeletePolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });
            
            options.AddPolicy(
                "ToDoPolicy",
                authBuilder =>
                {
                    authBuilder.RequireAuthenticatedUser();
                });


            // add other policies here 


            return options;
        }

    }
}
