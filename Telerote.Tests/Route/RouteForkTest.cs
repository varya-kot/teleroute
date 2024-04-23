using Teleroute.Route;
using Teleroute.Tests.Command;
using Teleroute.Tests.Match;
using Teleroute.Tests.Send;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Route
{
    public class RouteForkTest
    {
        [Test]
        public void RouteOriginCmdWhenMatchAndNoSpareCmd()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(true),
                    new FkCmd()
                ).Route(new FkWrap()).Single(),
                Is.EqualTo(new FkCmd()));
        }

        [Test]
        public void RouteEmptyWhenNotMatchAndNoSpareCmd()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(false),
                    new FkCmd()
                ).Route(new FkWrap()).Any(),
                Is.False);
        }

        [Test]
        public void RouteOriginCmdWhenMatch()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(true),
                    new FkCmd(),
                    new FkCmd(new FkSend("test"))
                ).Route(new FkWrap()).Single(),
                Is.EqualTo(new FkCmd()));
        }

        [Test]
        public void RouteSpareCmdWhenNotMatch()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(false),
                    new FkCmd(),
                    new FkCmd(new FkSend("test"))
                ).Route(new FkWrap()).Single(),
                Is.EqualTo(new FkCmd(new FkSend("test"))));
        }

        [Test]
        public void RouteOriginWhenMatchAndNoSpareRoute()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(true),
                    new RouteEnd<string, FkClient>(new FkCmd())
                ).Route(new FkWrap()).Single(),
                Is.EqualTo(new FkCmd()));
        }

        [Test]
        public void RouteEmptyWhenNotMatchAndNoSpareRoute()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(false),
                    new RouteEnd<string, FkClient>(new FkCmd())
                ).Route(new FkWrap()).Any(),
                Is.False);
        }

        [Test]
        public void RouteOriginWhenMatch()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(true),
                    new RouteEnd<string, FkClient>(new FkCmd()),
                    new RouteEnd<string, FkClient>(
                        new FkCmd(new FkSend("test"))
                    )
                ).Route(new FkWrap()).Single(),
                Is.EqualTo(new FkCmd()));
        }

        [Test]
        public void RouteSpareWhenNotMatch()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(false),
                    new RouteEnd<string, FkClient>(new FkCmd()),
                    new RouteEnd<string, FkClient>(
                        new FkCmd(new FkSend("test"))
                    )
                ).Route(new FkWrap()).Single(),
                Is.EqualTo(new FkCmd(new FkSend("test"))));
        }
    }
}
