using Teleroute.Command;
using Teleroute.Send;
using Teleroute.Tests.Send;

namespace Teleroute.Tests.Command
{
    public sealed class FkCmd : ICmd<string, FkClient>
    {
        private readonly IEnumerable<ISend<FkClient>> send;

        public FkCmd()
            : this(Enumerable.Empty<ISend<FkClient>>()) { }

        public FkCmd(ISend<FkClient> send)
            : this(new List<ISend<FkClient>>() { send }) { }

        private FkCmd(IEnumerable<ISend<FkClient>> send)
        {
            this.send = send;
        }

        public IEnumerable<ISend<FkClient>> Execute(string update)
        {
            return send;
        }

        public override bool Equals(object? obj)
        {
            return obj is FkCmd cmd && send.SequenceEqual(cmd.send);
        }

        public override int GetHashCode()
        {
            return send.GetHashCode();
        }
    }
}
