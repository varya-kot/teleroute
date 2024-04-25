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

namespace Teleroute.Send
{
    /// <summary>
    /// Sends many commands as one.
    /// </summary>
    /// <typeparam name="C">Client</typeparam>
    public sealed class SendBatch<C> : ISend<C>
    {
        private readonly IEnumerable<ISend<C>> sends;

        /// <summary>
        /// <example>
        /// <code>
        /// new SendBatch(
        ///     new YourSend(),
        ///     new YourAnotherSend(),
        ///     ...
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="sends">Sends</param>
        public SendBatch(params ISend<C>[] sends)
            : this(sends.ToList()) { }

        /// <summary>
        /// <example>
        /// <code>
        /// new SendBatch(
        ///     new ISend[]
        ///     {
        ///         new YourSend(),
        ///         new YourAnotherSend(),
        ///         ...
        ///     }
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="sends">Sends</param>
        public SendBatch(IEnumerable<ISend<C>> sends)
        {
            this.sends = sends;
        }

        public void Send(C client)
        {
            foreach (ISend<C> send in sends)
                try
                {
                    send.Send(client);
                }
                catch (Exception ex)
                {
                    //todo logger
                    Console.WriteLine(ex.ToString());
                }
        }
    }
}
