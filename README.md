[![EO principles respected here](https://www.elegantobjects.org/badge.svg)](https://www.elegantobjects.org)

[![codecov](https://codecov.io/gh/varya-kot/teleroute/graph/badge.svg?token=G4M5GKNBHC)](https://codecov.io/gh/varya-kot/teleroute)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/856c14036291498b8c7b106568d098ad)](https://app.codacy.com/gh/varya-kot/teleroute/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)

## Description

This is C# port of Java [teleroute](https://github.com/ArtemGet/teleroute) library

`IRoute` and `ICmd` interfaces return `IEnumerable`, that contains one element or nothing. This is done to avoid null checks in the code.

```cs
// find suitable command for update in route tree
IEnumerable<YourCommand> command = route.Route(update);

if(!command.Any())
{
  // not found processing or just ignore
}

IEnumerable<YourSend> send = command.Single().Execute(update);

// send your message to Telegram
send.Single().Send(yourTgClient);
```
