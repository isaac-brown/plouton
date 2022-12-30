// <copyright file="IdGenerator.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Plouton.Domain;

public abstract class IdGenerator
{
    public abstract Task<int> NextIdAsync();
}