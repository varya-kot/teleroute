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
using Teleroute.Send;

namespace Teleroute.Command
{
    /// <summary>
    /// Execute many commands as one.
    /// </summary>
    /// <typeparam name="U">Update</typeparam>
    /// <typeparam name="C">Client</typeparam>
    public sealed class CmdBatch<U, C> : ICmd<U, C>
    {
        private readonly IEnumerable<ICmd<U, C>> commands;

        /// <summary>
        /// <example>
        /// <code>
        /// new CmdBatch(
        ///     new YourCommand(),
        ///     new YourAnotherCommand(),
        ///     ...
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="commands">Commands to execute</param>
        public CmdBatch(params ICmd<U, C>[] commands)
            : this(commands.ToList()) { }

        /// <summary>
        /// <example>
        /// <code>
        /// new CmdBatch(
        ///     new ICmd[]
        ///     {
        ///         new YourCommand(),
        ///         new YourAnotherCommand(),
        ///         ...
        ///     }
        /// )
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="commands">Commands to execute</param>
        public CmdBatch(IEnumerable<ICmd<U, C>> commands)
        {
            this.commands = commands;
        }

        public IEnumerable<ISend<C>> Execute(U update)
        {
            List<ISend<C>> sends = ExecuteCmds(update).ToList();
            IEnumerable<ISend<C>> send;
            if (sends.Any())
            {
                send = new List<SendBatch<C>>(1) { new SendBatch<C>(sends) };
            }
            else
            {
                send = Enumerable.Empty<ISend<C>>();
            }

            return send;
        }

        private IEnumerable<ISend<C>> ExecuteCmds(U update)
        {
            return commands.Select(
                c =>
                {
                    IEnumerable<ISend<C>> sends;
                    try
                    {
                        sends = c.Execute(update);
                    }
                    catch (Exception ex)
                    {
                        //todo logger
                        Console.WriteLine(ex.ToString());
                        sends = Enumerable.Empty<ISend<C>>();
                    }

                    return sends;
                }).SelectMany(c => c);
        }
    }
}
