using Teleroute.Match;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Match
{
    public class MatchAnyTest
    {
        [Test]
        public void Match_OneMatch_ReturnsTrue()
        {
            Assert.That(
                new MatchAny<string>(
                    new FkMatch(true),
                    new FkMatch(false)
                ).Match(new FkWrap()),
                Is.True);
        }

        [Test]
        public void Match_NoCondition_ReturnsTrue()
        {
            Assert.That(
                new MatchAny<string>()
                .Match(new FkWrap()),
                Is.True);
        }

        [Test]
        public void Match_NoOneMatch_ReturnsFalse()
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
