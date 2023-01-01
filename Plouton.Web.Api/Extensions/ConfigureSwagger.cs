// <copyright file="ConfigureSwagger.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Reflection;

namespace Plouton.Web.Api.Extensions;

/// <summary>
/// Extension methods to configure swagger/Open API specification generated.
/// </summary>
public static class ConfigureSwagger
{
    /// <summary>
    /// Adds swagger related services to document the Web API.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>An <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            // Set the comments path for the XmlComments file.
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlFilePath, includeControllerXmlComments: true);
        });

        return services;
    }
}