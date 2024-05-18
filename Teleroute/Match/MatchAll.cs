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
    /// Match all conditions.
    /// </summary>
    /// <typeparam name="U">Update</typeparam>
    public class MatchAll<U> : IMatch<U>
    {
        private readonly IEnumerable<IMatch<U>> matches;

        /// <summary>
        /// <example>
        /// <code>
        /// new MatchAll(
        ///     new YourMatch(),
        ///     new YourAnotherMatch(),
        ///     ...
        /// );
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="matches">Conditions</param>
        public MatchAll(params IMatch<U>[] matches)
            : this(matches.ToList())
        {
        }

        /// <summary>
        /// <example>
        /// <code>
        /// new MatchAll(
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
        public MatchAll(IEnumerable<IMatch<U>> matches)
        {
            this.matches = matches;
        }

        /// <inheritdoc />
        public bool Match(IWrap<U> update)
        {
            return matches.All(m => m.Match(update));
        }
    }
}
