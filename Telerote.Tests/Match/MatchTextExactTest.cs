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

using Teleroute.Match;
using Teleroute.Tests.Update;

namespace Teleroute.Tests.Match;

/// <summary>
/// Test case <see cref="MatchTextExact{U}" />
/// </summary>
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
