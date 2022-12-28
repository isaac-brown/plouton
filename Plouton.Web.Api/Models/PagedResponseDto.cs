// <copyright file="PagedResponse.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Web.Api.Models;

public class PagedResponseDto<T>
{
    public IReadOnlyList<T> Items { get; set; }

    public int CountItems => this.Items.Count;

    public int Limit { get; set; }

    public string? Token { get; set; }
}