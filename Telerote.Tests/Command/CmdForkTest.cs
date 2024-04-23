using Teleroute.Command;
using Teleroute.Tests.Send;

namespace Teleroute.Tests.Command
{
    public class CmdForkTest
    {
        [Test]
        public void SendSpareWhenOriginError()
        {
            Assert.That(
                new CmdFork<string, FkClient>(
                    new FkCmdError(),
                    new FkCmd(new FkSend("spare"))
                ).Execute("test").Single(),
                Is.EqualTo(new FkSend("spare")));
        }

        [Test]
        public void SendOriginWhenNoError()
        {
            Assert.That(
                new CmdFork<string, FkClient>(
                    new FkCmd(new FkSend("origin")),
                    new FkCmd(new FkSend("spare"))
                ).Execute("test").Single(),
                Is.EqualTo(new FkSend("origin")));
        }
    }
}
