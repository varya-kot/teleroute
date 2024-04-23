using System;
using System.Collections.Generic;
using Teleroute.Send;

namespace Teleroute.Command
{
    public sealed class CmdFork<U, C> : ICmd<U, C>
    {
        private readonly ICmd<U, C> origin;
        private readonly ICmd<U, C> spare;

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
