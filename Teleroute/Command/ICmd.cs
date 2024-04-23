using System.Collections.Generic;

using Teleroute.Send;

namespace Teleroute.Command
{
    public interface ICmd<Update, Client>
    {
        IEnumerable<ISend<Client>> Execute(Update update);
    }
}
