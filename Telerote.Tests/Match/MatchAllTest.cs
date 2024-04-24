using Teleroute.Match;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Match
{
    public class MatchAllTest
    {
        [Test]
        public void Match_AllMatch_ReturnsTrue()
        {
            Assert.That(
                new MatchAll<string>(
                    new FkMatch(true),
                    new FkMatch(true)
                ).Match(new FkWrap()),
                Is.True);
        }

        [Test]
        public void Match_NoOneMatch_ReturnsFalse()
        {
            Assert.That(
                new MatchAll<string>(
                    new FkMatch(false),
                    new FkMatch(false)
                ).Match(new FkWrap()),
                Is.False);
        }

        [Test]
        public void Match_NotAllMatch_ReturnsFalse()
        {
            Assert.That(
                new MatchAll<string>(
                    new FkMatch(true),
                    new FkMatch(false)
                ).Match(new FkWrap()),
                Is.False);
        }

        [Test]
        public void Match_NoCondition_ReturnsTrue()
        {
            Assert.That(
                new MatchAll<string>()
                .Match(new FkWrap()),
                Is.True);
        }
    }
}
