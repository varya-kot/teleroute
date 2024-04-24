using Teleroute.Route;
using Teleroute.Tests.Command;
using Teleroute.Tests.Send;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Route
{
    public class RouteDfsTest
    {
        [Test]
        public void Route_ManyRouteSpecified_ReturnsFirstSuitable()
        {
            Assert.That(
              new RouteDfs<string, FkClient>(
                  new RouteEnd<string, FkClient>(),
                  new RouteEnd<string, FkClient>(new FkCmd(new FkSend("test1"))),
                  new RouteEnd<string, FkClient>(new FkCmd(new FkSend("test2")))
              ).Route(new FkWrap()).Single(),
              Is.EqualTo(new FkCmd(new FkSend("test1"))));
        }

        [Test]
        public void Route_OneRouteSpecified_ReturnsFirst()
        {
            Assert.That(
              new RouteDfs<string, FkClient>(
                  new RouteEnd<string, FkClient>(new FkCmd(new FkSend("test")))
              ).Route(new FkWrap()).Single(),
              Is.EqualTo(new FkCmd(new FkSend("test"))));
        }

        [Test]
        public void Route_NoRouteSpecified_ReturnsEmpty()
        {
            Assert.That(
               new RouteDfs<string, FkClient>(
                   new RouteEnd<string, FkClient>()
               ).Route(new FkWrap()).Any(),
               Is.False);
        }
    }
}
