using Teleroute.Match;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Match
{
    public class MatchAnyTest
    {
        [Test]
        public void MatchWhenOneMatch()
        {
            Assert.That(
                new MatchAny<string>(
                    new FkMatch(true),
                    new FkMatch(false)
                ).Match(new FkWrap()),
                Is.True);
        }

        [Test]
        public void MatchWhenNoCondition()
        {
            Assert.That(
                new MatchAny<string>()
                .Match(new FkWrap()),
                Is.True);
        }

        [Test]
        public void NotMatchWhenNoOneMatch()
        {
            Assert.That(
                new MatchAny<string>(
                    new FkMatch(false),
                    new FkMatch(false)
                ).Match(new FkWrap()),
                Is.False);
        }
    }
}
