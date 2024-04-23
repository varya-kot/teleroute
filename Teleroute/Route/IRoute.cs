using System.Collections.Generic;
using Teleroute.Command;
using Teleroute.Update;

namespace Teleroute.Route
{
    public interface IRoute<Update, Client>
    {
        IEnumerable<ICmd<Update, Client>> Route(IWrap<Update> update);
    }
}
