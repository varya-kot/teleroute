using System.Collections.Generic;
using System.Linq;
using Teleroute.Command;
using Teleroute.Update;

namespace Teleroute.Route
{
    public sealed class RouteEnd<U, C> : IRoute<U, C>
    {
        private readonly IEnumerable<ICmd<U, C>> command;

        public RouteEnd()
            : this(Enumerable.Empty<ICmd<U, C>>()) { }

        public RouteEnd(ICmd<U, C> command)
            : this(new List<ICmd<U, C>>(1) { command }) { }

        private RouteEnd(IEnumerable<ICmd<U, C>> command)
        {
            this.command = command;
        }

        public IEnumerable<ICmd<U, C>> Route(IWrap<U> update)
        {
            return command;
        }
    }
}
