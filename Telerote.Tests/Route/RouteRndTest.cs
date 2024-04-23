using Teleroute.Command;
using Teleroute.Route;
using Teleroute.Tests.Command;
using Teleroute.Tests.Send;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Route
{
    public class RouteRndTest
    {
        private HashSet<ICmd<string, FkClient>> commands;

        [SetUp]
        public void SetUp()
        {
            commands =
            [
                new FkCmd(),
                new FkCmd(new FkSend("test"))
            ];
        }

        [Test]
        public void RouteAnyWhenManyCmdSpecified()
        {
            Assert.That(commands,
                Does.Contain(
                    new RouteRnd<string, FkClient>(
                        new FkCmd(),
                        new FkCmd(new FkSend("test"))
                    ).Route(new FkWrap()).Single()));
        }

        [Test]
        public void RouteAnyWhenManyRouteSpecified()
        {
            Assert.That(commands,
                Does.Contain(
                    new RouteRnd<string, FkClient>(
                        new RouteEnd<string, FkClient>(new FkCmd()),
                        new RouteEnd<string, FkClient>(
                            new FkCmd(new FkSend("test"))
                        )
                    ).Route(new FkWrap()).Single()));
        }

        [Test]
        public void RouteWhenOneRouteSpecified()
        {
            Assert.That(
                new RouteRnd<string, FkClient>(
                    new RouteEnd<string, FkClient>(new FkCmd())
                ).Route(new FkWrap()).Single(),
                Is.EqualTo(new FkCmd()));
        }

        [Test]
        public void RouteWhenOneCmdSpecified()
        {
            Assert.That(
                new RouteRnd<string, FkClient>(new FkCmd())
                .Route(new FkWrap()).Single(),
                Is.EqualTo(new FkCmd()));
        }
    }
}
