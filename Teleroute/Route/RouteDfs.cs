// MIT License
// 
// Copyright (c) 2024 Varvara Getmanskaya
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;
using System.Linq;
using Teleroute.Command;
using Teleroute.Update;

namespace Teleroute.Route
{
    /// <summary>
    /// Depth first search route.
    /// Iterate over routes, pick first successful result.
    /// </summary>
    /// <typeparam name="U">Update</typeparam>
    /// <typeparam name="C">Client</typeparam>
    public sealed class RouteDfs<U, C> : IRoute<U, C>
    {
        private readonly IEnumerable<IRoute<U, C>> routes;

        /// <summary>
        /// <example>
        /// <code>
        /// new RouteDfs(
        ///     new RouteFork(
        ///         ...
        ///     ),
        ///     new RouteDfs(
        ///         ...
        ///     ),
        ///     ...,
        ///     new RouteEnd(
        ///         new NotFoundCommand()
        ///     )
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="routes">Routes</param>
        public RouteDfs(params IRoute<U, C>[] routes)
            : this(routes.ToList())
        {
        }

        /// <summary>
        /// <example>
        /// <code>
        /// new RouteDfs(
        ///     new IRoute[]
        ///     {
        ///         YourRoute(),
        ///         ...
        ///     }
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="routes">Routes</param>
        public RouteDfs(IEnumerable<IRoute<U, C>> routes)
        {
            this.routes = routes;
        }

        /// <inheritdoc />
        public IEnumerable<ICmd<U, C>> Route(IWrap<U> update)
        {
            return routes
                .Select(r => r.Route(update))
                .Where(c => c.Any())
                .DefaultIfEmpty(Enumerable.Empty<ICmd<U, C>>())
                .First();
        }
    }
}
