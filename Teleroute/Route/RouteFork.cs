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
using Teleroute.Command;
using Teleroute.Match;
using Teleroute.Update;

namespace Teleroute.Route
{
    /// <summary>
    /// Fork route. Pick origin or spare route.
    /// </summary>
    /// <typeparam name="U">Update</typeparam>
    /// <typeparam name="C">Client</typeparam>
    public sealed class RouteFork<U, C> : IRoute<U, C>
    {
        private readonly IMatch<U> match;
        private readonly IRoute<U, C> origin;
        private readonly IRoute<U, C> spare;

        /// <summary>
        /// Fork between one command and empty return in case of mismatch.
        /// <example>
        /// <code>
        /// new RouteFork(
        ///     new YourMatch(),
        ///     new YourCommand()
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="match">Condition</param>
        /// <param name="origin">Command</param>
        public RouteFork(IMatch<U> match, ICmd<U, C> origin)
            : this(match, new RouteEnd<U, C>(origin)) { }

        /// <summary>
        /// Fork between one route and empty return in case of mismatch.
        /// <example>
        /// <code>
        /// new RouteFork(
        ///     new YourMatch(),
        ///     new RouteDfs(
        ///         ...
        ///     )
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="match">Condition</param>
        /// <param name="origin">Route</param>
        public RouteFork(IMatch<U> match, IRoute<U, C> origin)
            : this(match, origin, new RouteEnd<U, C>()) { }

        /// <summary>
        /// Fork between two commands.
        /// <example>
        /// <code>
        /// new RouteFork(
        ///     new YourMatch(),
        ///     new YourCommand(),
        ///     new YourSpareCommand()
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="match">Condition</param>
        /// <param name="origin">Command</param>
        /// <param name="spare">Command</param>
        public RouteFork(IMatch<U> match, ICmd<U, C> origin, ICmd<U, C> spare)
            : this(match, new RouteEnd<U, C>(origin), new RouteEnd<U, C>(spare)) { }

        /// <summary>
        /// Fork between two routes.
        /// <example>
        /// <code>
        /// new RouteFork(
        ///     new YourMatch(),
        ///     new RouteDfs(
        ///         ...
        ///     ),
        ///     new RouteFork(
        ///         ...
        ///     )
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="match">Condition</param>
        /// <param name="origin">Route</param>
        /// <param name="spare">Route</param>
        public RouteFork(IMatch<U> match, IRoute<U, C> origin, IRoute<U, C> spare)
        {
            this.match = match;
            this.origin = origin;
            this.spare = spare;
        }

        public IEnumerable<ICmd<U, C>> Route(IWrap<U> update)
        {
            IEnumerable<ICmd<U, C>> command;
            if (match.Match(update))
            {
                command = origin.Route(update);
            }
            else
            {
                command = spare.Route(update);
            }
            return command;
        }
    }
}
