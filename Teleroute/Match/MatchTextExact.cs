using System.Linq;
using Teleroute.Update;

namespace Teleroute.Match
{
    public sealed class MatchTextExact<U> : IMatch<U>
    {
        private readonly string text;

        public MatchTextExact(string text)
        {
            this.text = text;
        }

        public bool Match(IWrap<U> update)
        {
            return update.Text().Any(t => t.Equals(text));
        }
    }
}
