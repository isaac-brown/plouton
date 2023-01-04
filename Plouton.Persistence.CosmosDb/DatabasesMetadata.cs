// <copyright file="DatabasesMetadata.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.Azure.Cosmos;

namespace Plouton.Persistence.CosmosDb;

/// <summary>
/// Provides helpful information about the structure of Cosmos databases used to implement Plouton.
/// </summary>
public static class DatabasesMetadata
{
    /// <summary>
    /// Represents metadata about the 'Plouton' database.
    /// </summary>
    /// <remarks>This is mostly here to stop usage of magic strings around the place.</remarks>
    public static class Plouton
    {
        /// <summary>
        /// The name of the database.
        /// </summary>
        public const string Name = "Plouton";

        /// <summary>
        /// Represents metadata about collections in the 'Plouton' database.
        /// </summary>
        public static class Collections
        {
            /// <summary>
            /// Represents metadata about collection 'Counters' in the 'Plouton' database.
            /// </summary>
            public static class Counters
            {
                /// <summary>
                /// The name of the collection.
                /// </summary>
                public const string Name = "Counters";

                /// <summary>
                /// Gets the <see cref="ContainerProperties"/> for this collection.
                /// </summary>
                public static ContainerProperties ContainerProperties { get; } = new(Counters.Name, "/id");
            }

            /// <summary>
            /// Represents metadata about collection 'Invoices' in the 'Plouton' database.
            /// </summary>
            public static class Invoices
            {
                /// <summary>
                /// The name of the collection.
                /// </summary>
                public const string Name = "Invoices";

                /// <summary>
                /// Gets the <see cref="ContainerProperties"/> for this collection.
                /// </summary>
                public static ContainerProperties ContainerProperties { get; } = new(Invoices.Name, "/id");
            }
        }
    }
}