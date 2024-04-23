using Teleroute.Send;

namespace Teleroute.Tests.Send
{
    public class SendBatchTest
    {
        private FkClient client;

        [SetUp]
        public void Setup()
        {
            client = new FkClient();
        }

        [Test]
        public void SentOneWhenSubmittedOneSend()
        {
            new SendBatch<FkClient>(new FkSend("test"))
                .Send(client);

            Assert.That(
                client.Sent(),
                Is.EqualTo(new List<string>() { "test" })
            );
        }

        [Test]
        public void SentManyWhenSubmittedMany()
        {
            new SendBatch<FkClient>(
                new FkSend("test1"),
                new FkSend("test2"),
                new FkSend("test3")
            ).Send(client);

            Assert.That(
                client.Sent(),
                Is.EqualTo(new List<string>() { "test1", "test2", "test3" })
            );
        }

        [Test]
        public void NothingSentWhenError()
        {
            new SendBatch<FkClient>(new FkSendError()).Send(client);

            Assert.That(
                client.Sent(),
                Is.EqualTo(new List<string>())
            );
        }

        [Test]
        public void SentOneWhenOtherError()
        {
            new SendBatch<FkClient>(
                new FkSend("test"),
                new FkSendError(),
                new FkSendError()
            ).Send(client);

            Assert.That(
                client.Sent(),
                Is.EqualTo(new List<string>() { "test" })
            );
        }
    }
}
