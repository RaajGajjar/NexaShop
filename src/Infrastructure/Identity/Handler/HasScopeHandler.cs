﻿using Microsoft.AspNetCore.Authorization;
using NexaShopsBackend.Infrastructure.Identity.Requirement;

namespace NexaShopsBackend.Infrastructure.Identity.Handler;

public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      HasScopeRequirement requirement
    )
    {
        // If user does not have the scope claim, get out of here
        if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
            return Task.CompletedTask;

        // Split the scopes string into an array
        #pragma warning disable CS8602 // Dereference of a possibly null reference.
        var scopes = context.User
          .FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer).Value.Split(' ');
        #pragma warning restore CS8602 // Dereference of a possibly null reference.

        // Succeed if the scope array contains the required scope
        if (scopes.Any(s => s == requirement.Scope))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
