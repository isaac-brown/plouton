// <copyright file="ConfigureAuthz.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Plouton.Web.Api.Extensions;

/// <summary>
/// Configure authentication and authorization services (aka Authz).
/// </summary>
public static class ConfigureAuthz
{
    /// <summary>
    /// Configure authentication and authorization services for the given <paramref name="services"/> instance.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">Used to source configuration values from.</param>
    /// <returns>An <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddAuthz(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Keycloak as the authentication and authorization provider.
        services.AddKeycloakAuthentication(configuration);
        services.AddKeycloakAuthorization(configuration);

        // Configure authorization policies.
        services.AddAuthorization(o =>
        {
            // If no policy is specified, require that the caller is authenticated as a minimum.
            o.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            o.AddPolicy("IsInvoiceReader", policy =>
            {
                // realm_access.roles
                policy.RequireRealmRoles("invoice:read", "invoice:write", "invoice:delete");

                // resource_access.<resource-name>.roles
                // policy.RequireResourceRoles("invoice:read", "invoice:write");
            });

            o.AddPolicy("IsInvoiceWriter", policy =>
            {
                policy.RequireRealmRoles("invoice:write", "invoice:delete");
            });

            o.AddPolicy("IsInvoiceDeleter", policy =>
            {
                policy.RequireRealmRoles("invoice:delete");
            });
        });

        return services;
    }
}
