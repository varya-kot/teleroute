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

using System;
using System.Collections.Generic;
using System.Linq;
using Teleroute.Command;
using Teleroute.Update;

namespace Teleroute.Route
{
    /// <summary>
    /// Pick any random route or command.
    /// </summary>
    /// <typeparam name="U">Update</typeparam>
    /// <typeparam name="C">Client</typeparam>
    public sealed class RouteRnd<U, C> : IRoute<U, C>
    {
        private readonly IEnumerable<IRoute<U, C>> routes;

        /// <summary>
        /// Pick any random command.
        /// <example>
        /// <code>
        /// new RouteRnd(
        ///     new YourCommand(),
        ///     new YourAnotherCommand(),
        ///     ...
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="commands">Commands</param>
        public RouteRnd(params ICmd<U, C>[] commands)
            : this(commands.Select(c => new RouteEnd<U, C>(c))) { }

        /// <summary>
        /// Pick any random route.
        /// <example>
        /// <code>
        /// new RouteRnd(
        ///     new RouteFork(
        ///         ...
        ///     ),
        ///     new YourRoute(),
        ///     ...
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="routes">Routes</param>
        public RouteRnd(params IRoute<U, C>[] routes)
            : this(routes.ToList()) { }

        /// <summary>
        /// Pick any random route.
        /// <example>
        /// <code>
        ///  new RouteRnd(
        ///      new IRoute[]
        ///      {
        ///          new YourRoute(),
        ///          ...
        ///      }
        ///  )
        ///  </code>
        /// </example>
        /// </summary>
        /// <param name="routes">Routes</param>
        public RouteRnd(IEnumerable<IRoute<U, C>> routes)
        {
            this.routes = routes;
        }

        public IEnumerable<ICmd<U, C>> Route(IWrap<U> update)
        {
            return routes
                .Skip(new Random().Next(routes.Count()))
                .First()
                .Route(update);
        }
    }
}
