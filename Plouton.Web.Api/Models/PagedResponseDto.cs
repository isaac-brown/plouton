// <copyright file="PagedResponseDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Web.Api.Models;

/// <summary>
/// Represents the JSON document which is used for displaying paged responses.
/// </summary>
/// <typeparam name="T">The type of items stored in this collection.</typeparam>
public class PagedResponseDto<T>
{
    // Disable some warnings as this class is a POCO used to map data to/from HTTP request/response bodies.
#pragma warning disable SA1600 //Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable property must contain a non-null value when exiting constructor.

    public IReadOnlyList<T> Items { get; set; }

    public int CountItems => this.Items.Count;

    public int Limit { get; set; }

    public string? Token { get; set; }

#pragma warning restore SA1600 //Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CS8618 // Non-nullable property must contain a non-null value when exiting constructor.
}