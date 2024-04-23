using System.Collections.Generic;
using System.Linq;
using Teleroute.Update;

namespace Teleroute.Match
{
    public class MatchAll<U> : IMatch<U>
    {
        private readonly IEnumerable<IMatch<U>> matches;

        public MatchAll(params IMatch<U>[] matches)
            : this(matches.ToList()) { }

        public MatchAll(IEnumerable<IMatch<U>> matches)
        {
            this.matches = matches;
        }

        public bool Match(IWrap<U> update)
        {
            return matches.All(m => m.Match(update));
        }
    }
}
