using Teleroute.Update;

namespace Teleroute.Tests.Update
{
    public sealed class FkWrap : IWrap<string>
    {
        private readonly int id;
        private readonly bool isCommand;
        private readonly string content;

        public FkWrap() : this(123, true, "text") { }

        public FkWrap(int id, bool isCommand, string content)
        {
            this.id = id;
            this.isCommand = isCommand;
            this.content = content;
        }

        public int Identity()
        {
            return id;
        }

        public bool IsCommand()
        {
            return isCommand;
        }

        public string Source()
        {
            return content;
        }

        public IEnumerable<string> Text()
        {
            return new List<string>(1) { content };
        }
    }
}
