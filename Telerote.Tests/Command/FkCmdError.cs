using Teleroute.Command;
using Teleroute.Send;
using Teleroute.Tests.Send;

namespace Teleroute.Tests.Command
{
    public sealed class FkCmdError : ICmd<string, FkClient>
    {
        public IEnumerable<ISend<FkClient>> Execute(string update)
        {
            throw new NotImplementedException();
        }
    }
}
