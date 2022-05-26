using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AspNetAuth;

static class PolicyName
{
    public const string OnlyUniUpo = "OnlyUniUpo";
    public const string Over20 = "Over20";
}

class UniUpoRequirement : IAuthorizationRequirement
{ }

class UniUpoHandler : AuthorizationHandler<UniUpoRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UniUpoRequirement requirement)
    {
        if (context.User?.Identity?.Name?.EndsWith("@uniupo.it") ?? false)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

record MinimumAgeRequirement(int MinimumAge) : IAuthorizationRequirement
{ }

class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var claim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);
        if (claim == null)
        {
            // Requisito non soddisfatto perchè non è nota la data di nascita dell'utente.
            return Task.FromResult(0);
        }

        var dateOfBirth = Convert.ToDateTime(claim.Value);
        int age = DateTime.Today.Year - dateOfBirth.Year;
        if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

        if (age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }

        return Task.FromResult(0);
    }
}

static class AuthExtensions
{
    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyName.OnlyUniUpo, policy => policy.AddRequirements(new UniUpoRequirement()));
            options.AddPolicy(PolicyName.Over20, policy => policy.AddRequirements(new MinimumAgeRequirement(20)));
        });

        services.AddSingleton<IAuthorizationHandler, UniUpoHandler>();
        services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
    }
}