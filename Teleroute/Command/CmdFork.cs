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
using Teleroute.Send;

namespace Teleroute.Command
{
    /// <summary>
    /// Fork command. Origin or spare if error.
    /// </summary>
    /// <typeparam name="U">Update</typeparam>
    /// <typeparam name="C">Client</typeparam>
    public sealed class CmdFork<U, C> : ICmd<U, C>
    {
        private readonly ICmd<U, C> origin;
        private readonly ICmd<U, C> spare;

        /// <summary>
        /// <example>
        /// <code>
        /// new CmdFork(
        ///     new YourOriginCommand(),
        ///     new YourSpareCommand()
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="origin">Command</param>
        /// <param name="spare">Command</param>
        public CmdFork(ICmd<U, C> origin, ICmd<U, C> spare)
        {
            this.origin = origin;
            this.spare = spare;
        }

        public IEnumerable<ISend<C>> Execute(U update)
        {
            try
            {
                return origin.Execute(update);
            }
            catch (Exception)
            {
                return spare.Execute(update);
            }
        }
    }
}
