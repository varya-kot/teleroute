using Teleroute.Send;

namespace Teleroute.Tests.Send
{
    public sealed class FkSendError : ISend<FkClient>
    {
        public void Send(FkClient client)
        {
            throw new NotImplementedException();
        }
    }
}
