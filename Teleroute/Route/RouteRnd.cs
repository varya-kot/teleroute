using System;
using System.Collections.Generic;
using System.Linq;
using Teleroute.Command;
using Teleroute.Update;

namespace Teleroute.Route
{
    public sealed class RouteRnd<U, C> : IRoute<U, C>
    {
        private readonly IEnumerable<IRoute<U, C>> routes;

        public RouteRnd(params ICmd<U, C>[] commands)
            : this(commands.Select(c => new RouteEnd<U, C>(c))) { }

        public RouteRnd(params IRoute<U, C>[] routes)
            : this(routes.ToList()) { }

        public RouteRnd(IEnumerable<IRoute<U, C>> routes)
        {
            this.routes = routes;
        }

        public IEnumerable<ICmd<U, C>> Route(IWrap<U> update)
        {
            return routes
                .Skip(new Random().Next(routes.Count()))
                .First()
                .Route(update);
        }
    }
}
