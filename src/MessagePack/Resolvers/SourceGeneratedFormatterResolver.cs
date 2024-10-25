// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using MessagePack.Formatters;
using MessagePack.Internal;

namespace MessagePack.Resolvers
{
    /// <summary>
    /// A resolver that discovers formatters generated by <c>MessagePack.SourceGenerator</c>.
    /// </summary>
    public sealed class SourceGeneratedFormatterResolver : IFormatterResolver
    {
        /// <summary>
        /// The singleton instance that can be used.
        /// </summary>
        public static readonly SourceGeneratedFormatterResolver Instance = new();

        private static readonly ConcurrentDictionary<Assembly, IFormatterResolver?> AssemblyResolverCache = new();

        private SourceGeneratedFormatterResolver()
        {
        }

        /// <inheritdoc/>
        public IMessagePackFormatter<T>? GetFormatter<T>() => FormatterCache<T>.Formatter;

        private static class FormatterCache<T>
        {
            internal static readonly IMessagePackFormatter<T>? Formatter = FindPrecompiledFormatter();

            private static IMessagePackFormatter<T>? FindPrecompiledFormatter()
            {
                IFormatterResolver? resolver = AssemblyResolverCache.GetOrAdd(typeof(T).Assembly, static assembly =>
                {
                    if (typeof(T).Assembly.GetCustomAttributes<GeneratedAssemblyMessagePackResolverAttribute>().FirstOrDefault() is { } att)
                    {
                        return (IFormatterResolver?)att.ResolverType.GetField("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
                    }

                    return null;
                });

                return resolver?.GetFormatter<T>();
            }
        }
    }
}
