// MIT License
//
// Copyright (c) 2024 Varvara Getmanskaya
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Teleroute.Send;

namespace Teleroute.Tests.Send;

/// <summary>
/// Test case <see cref="SendBatch{C}" />
/// </summary>
public class SendBatchTest
{
    private FkClient client;

    [SetUp]
    public void Setup()
    {
        client = new FkClient();
    }

    [Test]
    public void Send_SubmittedOneSend_SentOne()
    {
        new SendBatch<FkClient>(new FkSend("test"))
            .Send(client);

        Assert.That(
            client.Sent(),
            Is.EqualTo(new List<string> { "test" })
        );
    }

    [Test]
    public void Send_SubmittedManySend_SentMany()
    {
        new SendBatch<FkClient>(
            new FkSend("test1"),
            new FkSend("test2"),
            new FkSend("test3")
        ).Send(client);

        Assert.That(
            client.Sent(),
            Is.EqualTo(new List<string> { "test1", "test2", "test3" })
        );
    }

    [Test]
    public void Send_ErrorSend_SentNothing()
    {
        new SendBatch<FkClient>(new FkSendError()).Send(client);

        Assert.That(
            client.Sent(),
            Is.EqualTo(new List<string>())
        );
    }

    [Test]
    public void Send_OneSendAndOtherErrorSend_SentOne()
    {
        new SendBatch<FkClient>(
            new FkSend("test"),
            new FkSendError(),
            new FkSendError()
        ).Send(client);

        Assert.That(
            client.Sent(),
            Is.EqualTo(new List<string> { "test" })
        );
    }
}
