using System.Collections.Generic;
using System.Linq;
using Teleroute.Command;
using Teleroute.Update;

namespace Teleroute.Route
{
    public sealed class RouteDfs<U, C> : IRoute<U, C>
    {
        private readonly IEnumerable<IRoute<U, C>> routes;

        public RouteDfs(params IRoute<U, C>[] routes)
           : this(routes.ToList()) { }

        public RouteDfs(IEnumerable<IRoute<U, C>> routes)
        {
            this.routes = routes;
        }

        public IEnumerable<ICmd<U, C>> Route(IWrap<U> update)
        {
            return routes
                .Select(r => r.Route(update))
                .Where(c => c.Any())
                .DefaultIfEmpty(Enumerable.Empty<ICmd<U, C>>())
                .First();
        }
    }
}
