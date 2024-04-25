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

using Teleroute.Route;
using Teleroute.Tests.Command;
using Teleroute.Tests.Match;
using Teleroute.Tests.Send;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Route;

/// <summary>
/// Test case <see cref="RouteFork{U,C}" />
/// </summary>
public class RouteForkTest
{
    [Test]
    public void Route_MatchAndNoSpareCmd_ReturnsOrigin()
    {
        Assert.That(
            new RouteFork<string, FkClient>(
                new FkMatch(true),
                new FkCmd()
            ).Route(new FkWrap()).Single(),
            Is.EqualTo(new FkCmd())
        );
    }

    [Test]
    public void Route_NotMatchAndNoSpareCmd_ReturnsEmpty()
    {
        Assert.That(
            new RouteFork<string, FkClient>(
                new FkMatch(false),
                new FkCmd()
            ).Route(new FkWrap()).Any(),
            Is.False
        );
    }

    [Test]
    public void Route_MatchAndSpareCmd_ReturnsOrigin()
    {
        Assert.That(
            new RouteFork<string, FkClient>(
                new FkMatch(true),
                new FkCmd(),
                new FkCmd(new FkSend("test"))
            ).Route(new FkWrap()).Single(),
            Is.EqualTo(new FkCmd())
        );
    }

    [Test]
    public void Route_NotMatchAndSpareCmd_ReturnsSpare()
    {
        Assert.That(
            new RouteFork<string, FkClient>(
                new FkMatch(false),
                new FkCmd(),
                new FkCmd(new FkSend("test"))
            ).Route(new FkWrap()).Single(),
            Is.EqualTo(new FkCmd(new FkSend("test")))
        );
    }

    [Test]
    public void Route_MatchAndNoSpareRoute_ReturnsOrigin()
    {
        Assert.That(
            new RouteFork<string, FkClient>(
                new FkMatch(true),
                new RouteEnd<string, FkClient>(new FkCmd())
            ).Route(new FkWrap()).Single(),
            Is.EqualTo(new FkCmd())
        );
    }

    [Test]
    public void Route_NotMatchAndNoSpareRoute_ReturnsEmpty()
    {
        Assert.That(
            new RouteFork<string, FkClient>(
                new FkMatch(false),
                new RouteEnd<string, FkClient>(new FkCmd())
            ).Route(new FkWrap()).Any(),
            Is.False
        );
    }

    [Test]
    public void Route_MatchAndSpareRoute_ReturnsOrigin()
    {
        Assert.That(
            new RouteFork<string, FkClient>(
                new FkMatch(true),
                new RouteEnd<string, FkClient>(new FkCmd()),
                new RouteEnd<string, FkClient>(
                    new FkCmd(new FkSend("test"))
                )
            ).Route(new FkWrap()).Single(),
            Is.EqualTo(new FkCmd())
        );
    }

    [Test]
    public void Route_NotMatchAndSpareRoute_ReturnsSpare()
    {
        Assert.That(
            new RouteFork<string, FkClient>(
                new FkMatch(false),
                new RouteEnd<string, FkClient>(new FkCmd()),
                new RouteEnd<string, FkClient>(
                    new FkCmd(new FkSend("test"))
                )
            ).Route(new FkWrap()).Single(),
            Is.EqualTo(new FkCmd(new FkSend("test")))
        );
    }
}
