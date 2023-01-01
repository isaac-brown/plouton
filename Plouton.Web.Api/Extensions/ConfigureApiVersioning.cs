// <copyright file="ConfigureApiVersioning.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Mvc;

namespace Plouton.Web.Api.Extensions;

/// <summary>
/// Configure the API versioning properties of the project.
/// </summary>
public static class ConfigureApiVersioning
{
    /// <summary>
    /// Configure the API versioning properties of the project, such as return headers, version format, etc.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>An <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddApiVersioningConfigured(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            // ReportApiVersions will return the "api-supported-versions" and "api-deprecated-versions" headers.
            options.ReportApiVersions = true;

            // Set a default version when it's not provided,
            // e.g., for backward compatibility when applying versioning on existing APIs
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        // Support versioning on our documentation.
        services.AddVersionedApiExplorer(options =>
        {
            // Format the version as "v{Major}.{Minor}.{Patch}" (e.g. v1.0.0).
            options.GroupNameFormat = "'v'VVV";

            // Note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}