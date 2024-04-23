using System.Linq;
using Teleroute.Update;

namespace Teleroute.Match
{
    public sealed class MatchTextPart<U> : IMatch<U>
    {
        private readonly string text;

        public MatchTextPart(string text)
        {
            this.text = text;
        }

        public bool Match(IWrap<U> update)
        {
            return update.Text().Any(t => t.Contains(text));
        }
    }
}
