using Teleroute.Send;

namespace Teleroute.Tests.Send
{
    public sealed class FkSend : ISend<FkClient>
    {
        private readonly IEnumerable<string> response;

        public FkSend() : this(Enumerable.Empty<string>()) { }

        public FkSend(string response)
            : this(new List<string>() { response }) { }

        private FkSend(IEnumerable<string> response)
        {
            this.response = response;
        }

        public void Send(FkClient client)
        {
            foreach (string item in response)
            {
                client.Submit(item);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is FkSend send && response.SequenceEqual(send.response);
        }

        public override int GetHashCode()
        {
            return response.GetHashCode();
        }
    }
}
