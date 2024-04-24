using Teleroute.Command;
using Teleroute.Tests.Send;

namespace Teleroute.Tests.Command
{
    public class CmdBatchTest
    {
        [Test]
        public void Execute_ErrorCmd_ReturnsEmpty()
        {
            Assert.That(
                new CmdBatch<string, FkClient>(
                    new FkCmdError()
                ).Execute("test").Any(),
                Is.False);
        }

        [Test]
        public void Execute_SubmittedCmd_ReturnsSend()
        {
            Assert.That(
                new CmdBatch<string, FkClient>(
                    new FkCmd(new FkSend())
                ).Execute("test").Any(),
                Is.True);
        }
    }
}
