using System.Collections.Generic;
using System.Linq;
using Teleroute.Update;

namespace Teleroute.Match
{
    public sealed class MatchAny<U> : IMatch<U>
    {
        private readonly IEnumerable<IMatch<U>> matches;

        public MatchAny(params IMatch<U>[] matches)
            : this(matches.ToList()) { }

        public MatchAny(IEnumerable<IMatch<U>> matches)
        {
            this.matches = matches;
        }

        public bool Match(IWrap<U> update)
        {
            return !matches.Any() || matches.Any(m => m.Match(update));
        }
    }
}
