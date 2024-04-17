# Contributing to the Open IMO CII Calculator

Thanks for your interest in the project!

## Testing

There are some unit tests in the `EtiveMor.OpenImoCiiCalculator.Core.Tests` directory. The app uses MSTest for all unit testing. 

## Submitting Changes

When making changes, please send a pull request to Open-IMO-CII-Calculator with a clear list of what changes have been made. To understand more about pull requests, please see Github's [Pull Request help page](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/getting-started/best-practices-for-pull-requests).

Pull Requests should contain one feature/change per commit (atomic commits). Pull request messages should be clear and to the point.

Please be explicit where your contribution has used generative AI &/or LLMs to create code (including tools like github copilot), and describe which model was used. 

## Coding Conventions

- Prefer readability over performance
- Generally follow [dotnet coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Methods and classes should be commented with `/// <summary>` style comments describing the method's purpose
- If a method implements a regulatory requirement, for example `IMO: MEPC.354(78)`, please include a reference to the exact regulation name in the comments
