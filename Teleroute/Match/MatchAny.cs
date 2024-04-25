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
using Teleroute.Update;

namespace Teleroute.Match
{
    /// <summary>
    /// Match any condition.
    /// </summary>
    /// <typeparam name="U">Update</typeparam>
    public sealed class MatchAny<U> : IMatch<U>
    {
        private readonly IEnumerable<IMatch<U>> matches;

        /// <summary>
        /// <example>
        /// <code>
        /// new MatchAny(
        ///     new YourMatch(),
        ///     new YourAnotherMatch(),
        ///     ...
        /// );
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="matches">Conditions</param>
        public MatchAny(params IMatch<U>[] matches)
            : this(matches.ToList()) { }

        /// <summary>
        /// <example>
        /// <code>
        /// new MatchAny(
        ///     new IMatch[]
        ///     {
        ///         new YourMatch(),
        ///         new YourAnotherMatch(),
        ///         ...
        ///     }
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="matches">Conditions</param>
        public MatchAny(IEnumerable<IMatch<U>> matches)
        {
            this.matches = matches;
        }

        public bool Match(IWrap<U> update)
        {
            return !matches.Any() || matches.Any(m => m.Match(update));
        }
    }
}
