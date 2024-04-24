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
        public void Route_MatchAndNoSpareCmd_ReturnsOrigin()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(true),
                    new FkCmd()
                ).Route(new FkWrap()).Single(),
                Is.EqualTo(new FkCmd()));
        }

        [Test]
        public void Route_NotMatchAndNoSpareCmd_ReturnsEmpty()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(false),
                    new FkCmd()
                ).Route(new FkWrap()).Any(),
                Is.False);
        }

        [Test]
        public void Route_MatchAndSpareCmd_ReturnsOrigin()
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
        public void Route_NotMatchAndSpareCmd_ReturnsSpare()
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
        public void Route_MatchAndNoSpareRoute_ReturnsOrigin()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(true),
                    new RouteEnd<string, FkClient>(new FkCmd())
                ).Route(new FkWrap()).Single(),
                Is.EqualTo(new FkCmd()));
        }

        [Test]
        public void Route_NotMatchAndNoSpareRoute_ReturnsEmpty()
        {
            Assert.That(
                new RouteFork<string, FkClient>(
                    new FkMatch(false),
                    new RouteEnd<string, FkClient>(new FkCmd())
                ).Route(new FkWrap()).Any(),
                Is.False);
        }

        [Test]
        public void Route_MatchAndSpareRoute_ReturnsOrigin()
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
        public void Route_NotMatchAndSpareRoute_ReturnsSpare()
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
