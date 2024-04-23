using Teleroute.Match;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Match
{
    public class MatchCmdTest
    {
        [Test]
        public void MatchWhenCommand()
        {
            Assert.That(
                new MatchCmd<string>()
                .Match(new FkWrap(1, true, "content")),
                Is.True);
        }

        [Test]
        public void NotMatchWhenNotCommand()
        {
            Assert.That(
                new MatchCmd<string>()
                .Match(new FkWrap(1, false, "content")),
                Is.False);
        }
    }
}
