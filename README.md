# Simon.NET
A Simon game that supports multiple controllers. Originally based on Microsoft XNA Framework 4 and ported to MonoGame framework

<p align="center">
  <img src="https://github.com/xlfdll/xlfdll.github.io/raw/master/images/projects/Simon.NET.png"
       alt="Simon.NET">
</p>

## System Requirements
* .NET Framework 4.8

[Runtime configuration](https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-configure-an-app-to-support-net-framework-4-or-4-5) may be needed for running on other versions of .NET Framework.

## Usage
Just run and have fun ;-)

## Development Prerequisites
* Visual Studio 2015+

No need to install MonoGame SDK beforehand. The project will automatically restore MonoGame assemblies from NuGet during compilation.

Before the build, generate-build-number.sh needs to be executed in a Git / Bash shell to generate build information code file (`BuildInfo.cs`).
