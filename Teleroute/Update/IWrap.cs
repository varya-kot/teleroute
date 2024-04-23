using System.Collections.Generic;

namespace Teleroute.Update
{
    public interface IWrap<Update>
    {
        int Identity();
        bool IsCommand();
        IEnumerable<string> Text();
        Update Source();
    }
}
