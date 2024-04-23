using System.Collections.ObjectModel;

namespace Teleroute.Tests.Send
{
    public sealed class FkClient
    {
        private readonly IList<string> response;

        public FkClient()
            : this(new List<string>(1)) { }

        //public FkClient(string message)
        //    : this(new List<string> { message }) { }

        private FkClient(IList<string> responce)
        {
            this.response = responce;
        }

        public void Submit(string message)
        {
            this.response.Add(message);
        }

        public IList<string> Sent()
        {
            return new ReadOnlyCollection<string>(response);
        }
    }
}
