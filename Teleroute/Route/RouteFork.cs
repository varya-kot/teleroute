using System.Collections.Generic;
using Teleroute.Command;
using Teleroute.Match;
using Teleroute.Update;

namespace Teleroute.Route
{
    public sealed class RouteFork<U, C> : IRoute<U, C>
    {
        private readonly IMatch<U> match;
        private readonly IRoute<U, C> origin;
        private readonly IRoute<U, C> spare;
        public RouteFork(IMatch<U> match, ICmd<U, C> origin)
            : this(match, new RouteEnd<U, C>(origin)) { }

        public RouteFork(IMatch<U> match, IRoute<U, C> origin)
            : this(match, origin, new RouteEnd<U, C>()) { }

        public RouteFork(IMatch<U> match, ICmd<U, C> origin, ICmd<U, C> spare)
            : this(match, new RouteEnd<U, C>(origin), new RouteEnd<U, C>(spare)) { }

        public RouteFork(IMatch<U> match, IRoute<U, C> origin, IRoute<U, C> spare)
        {
            this.match = match;
            this.origin = origin;
            this.spare = spare;
        }

        public IEnumerable<ICmd<U, C>> Route(IWrap<U> update)
        {
            if (match.Match(update))
                return origin.Route(update);
            else
                return spare.Route(update);
        }
    }
}
