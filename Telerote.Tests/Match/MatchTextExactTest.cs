using Teleroute.Match;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Match
{
    public class MatchTextExactTest
    {
        [Test]
        public void MatchWhenFullTextMatch()
        {
            Assert.That(
                new MatchTextExact<string>("test")
                .Match(new FkWrap(1, true, "test")),
                Is.True);
        }

        [Test]
        public void NotMatchWhenFullTextNotMatch()
        {
            Assert.That(
                new MatchTextExact<string>("test")
                .Match(new FkWrap(1, true, "test1")),
                Is.False);
        }
    }
}
