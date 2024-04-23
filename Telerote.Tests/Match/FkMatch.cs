using Teleroute.Match;
using Teleroute.Update;

namespace Teleroute.Tests.Match
{
    public sealed class FkMatch : IMatch<string>
    {
        private readonly bool match;

        public FkMatch(bool match)
        {
            this.match = match;
        }

        public bool Match(IWrap<string> update)
        {
            return match;
        }
    }
}
