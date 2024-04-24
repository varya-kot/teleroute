using Teleroute.Match;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Match
{
    public class MatchTextExactTest
    {
        [Test]
        public void Match_FullTextMatch_ReturnsTrue()
        {
            Assert.That(
                new MatchTextExact<string>("test")
                .Match(new FkWrap(1, true, "test")),
                Is.True);
        }

        [Test]
        public void Match_NotFullTextMatch_ReturnsFalse()
        {
            Assert.That(
                new MatchTextExact<string>("test")
                .Match(new FkWrap(1, true, "test1")),
                Is.False);
        }
    }
}
