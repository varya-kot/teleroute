using Teleroute.Update;

namespace Teleroute.Match
{
    public interface IMatch<Update>
    {
        bool Match(IWrap<Update> update);
    }
}
