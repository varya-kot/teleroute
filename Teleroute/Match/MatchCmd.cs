using Teleroute.Update;

namespace Teleroute.Match
{
    public sealed class MatchCmd<U> : IMatch<U>
    {
        public bool Match(IWrap<U> update)
        {
            return update.IsCommand();
        }
    }
}
