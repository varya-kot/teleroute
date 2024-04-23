using Teleroute.Match;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Match
{
    public class MatchAllTest
    {
        [Test]
        public void MatchWhenAllMatch()
        {
            Assert.That(
                new MatchAll<string>(
                    new FkMatch(true),
                    new FkMatch(true)
                ).Match(new FkWrap()),
                Is.True);
        }

        [Test]
        public void NotMatchWhenNoOneMatch()
        {
            Assert.That(
                new MatchAll<string>(
                    new FkMatch(false),
                    new FkMatch(false)
                ).Match(new FkWrap()),
                Is.False);
        }

        [Test]
        public void NotMatchWhenNotAllMatch()
        {
            Assert.That(
                new MatchAll<string>(
                    new FkMatch(true),
                    new FkMatch(false)
                ).Match(new FkWrap()),
                Is.False);
        }

        [Test]
        public void MatchWhenNoCondition()
        {
            Assert.That(
                new MatchAll<string>()
                .Match(new FkWrap()),
                Is.True);
        }
    }
}
