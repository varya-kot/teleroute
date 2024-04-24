using Teleroute.Route;
using Teleroute.Tests.Command;
using Teleroute.Tests.Send;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Route
{
    public class RouteEndTest
    {
        [Test]
        public void Route_NoCmdSpecified_ReturnsEmpty()
        {
            Assert.That(
               new RouteEnd<string, FkClient>()
               .Route(new FkWrap()).Any(),
               Is.False);
        }

        [Test]
        public void Route_CmdSpecified_ReturnsCmd()
        {
            Assert.That(
               new RouteEnd<string, FkClient>(
                   new FkCmd()
               )
               .Route(new FkWrap()).Single(),
               Is.EqualTo(new FkCmd()));
        }
    }
}
