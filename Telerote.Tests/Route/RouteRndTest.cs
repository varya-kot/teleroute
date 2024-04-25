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

using Teleroute.Command;
using Teleroute.Route;
using Teleroute.Tests.Command;
using Teleroute.Tests.Send;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Route;

/// <summary>
/// Test case <see cref="RouteRnd{U,C}" />
/// </summary>
public class RouteRndTest
{
    private HashSet<ICmd<string, FkClient>> commands;

    [SetUp]
    public void SetUp()
    {
        commands =
        [
            new FkCmd(),
            new FkCmd(new FkSend("test"))
        ];
    }

    [Test]
    public void Route_ManyCmdSpecified_ReturnsAny()
    {
        Assert.That(
            commands,
            Does.Contain(
                new RouteRnd<string, FkClient>(
                    new FkCmd(),
                    new FkCmd(new FkSend("test"))
                ).Route(new FkWrap()).Single()
            )
        );
    }

    [Test]
    public void Route_ManyRouteSpecified_ReturnsAny()
    {
        Assert.That(
            commands,
            Does.Contain(
                new RouteRnd<string, FkClient>(
                    new RouteEnd<string, FkClient>(new FkCmd()),
                    new RouteEnd<string, FkClient>(
                        new FkCmd(new FkSend("test"))
                    )
                ).Route(new FkWrap()).Single()
            )
        );
    }

    [Test]
    public void Route_OneRouteSpecified_ReturnsSingle()
    {
        Assert.That(
            new RouteRnd<string, FkClient>(
                new RouteEnd<string, FkClient>(new FkCmd())
            ).Route(new FkWrap()).Single(),
            Is.EqualTo(new FkCmd())
        );
    }

    [Test]
    public void Route_OneCmdSpecified_ReturnsSingle()
    {
        Assert.That(
            new RouteRnd<string, FkClient>(new FkCmd())
                .Route(new FkWrap()).Single(),
            Is.EqualTo(new FkCmd())
        );
    }
}
