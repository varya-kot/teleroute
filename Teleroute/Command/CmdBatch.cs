using System;
using System.Collections.Generic;
using System.Linq;
using Teleroute.Send;

namespace Teleroute.Command
{
    public sealed class CmdBatch<U, C> : ICmd<U, C>
    {
        private readonly IEnumerable<ICmd<U, C>> commands;

        public CmdBatch(params ICmd<U, C>[] commands)
            : this(commands.ToList()) { }

        public CmdBatch(IEnumerable<ICmd<U, C>> commands)
        {
            this.commands = commands;
        }

        //todo logger
        public IEnumerable<ISend<C>> Execute(U update)
        {
            IEnumerable<ISend<C>> r = ExecuteCmds(update);
            if (r.Any())
                return new List<SendBatch<C>>(1) { new SendBatch<C>(r) };
            else
                return Enumerable.Empty<ISend<C>>();
        }

        private IEnumerable<ISend<C>> ExecuteCmds(U update)
        {
            return commands.Select(c =>
            {
                try
                {
                    return c.Execute(update);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return Enumerable.Empty<ISend<C>>();
                }
            }).Where(s => s.Any()).SelectMany(c => c);
        }
    }
}
