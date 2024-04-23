using Teleroute.Match;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Match
{
    public class MatchTextPartTest
    {
        [Test]
        public void MatchWhenFullTextMatch()
        {
            Assert.That(
                new MatchTextPart<string>("test")
                .Match(new FkWrap(1, true, "test")),
                Is.True);
        }

        [Test]
        public void MatchWhenTextOccurs()
        {
            Assert.That(
                new MatchTextPart<string>("te")
                .Match(new FkWrap(1, true, "test")),
                Is.True);
        }

        [Test]
        public void NotMatchWhenTextNotOccurs()
        {
            Assert.That(
                new MatchTextPart<string>("no")
                .Match(new FkWrap(1, true, "test")),
                Is.False);
        }
    }
}
