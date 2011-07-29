------
LINEUP
------


-----
INTRO

How many times have you had to run msbuild, aspnet_regiis etc from the command line and ended up browsing to the directory in Explorer, then copying and pasting the path into your console? 

LineUp wants to solve this problem by temporarily adding paths to your PATH environment variable using as few keystrokes as possible.

During command prompt session, you can type lu setpath <component> <version> it will add the relevant path to your PATH. LineUp only changes the PATH within the scope of your command prompt session. It does not make permanent changes to your system.


Example usage:
___________________________________________________________________________________________________
Microsoft Windows [Version 6.1.7601]
Copyright (c) 2009 Microsoft Corporation.  All rights reserved.

D:\Dev>where msbuild
INFO: Could not find files for the given pattern(s).

D:\Dev>lu setpath .net 4.0
Running LineUp
OK: Set path "C:\Windows\Microsoft.NET\Framework\v4.0.30319\" for component ".net", version "4.0"

D:\Dev>where msbuild
C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe

D:\Dev>lu setpath .net 3.5
Running LineUp
OK: Set path "C:\Windows\Microsoft.NET\Framework\v3.5\" for component ".net", version "3.5"

D:\Dev>where msbuild
C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe

D:\Dev>
___________________________________________________________________________________________________

The options are hardcoded for now (see DemoConfigurationBuilder) and just contain the main .net framework versions:

lu setpath .net 4.0
lu setpath .net 3.5
lu setpath .net 2.0

It is intended to support all sorts of other tools that you might not want permanently in your machine's path 


Inspired by pik - http://rubygems.org/gems/pik


----
NOTE

This is prototype code written in 1h. It works on my machine. It has no tests or anything.


------------
INSTALLATION

1. Build project
2. Copy bin\Release\lu.bat and  bin\Release\LineUp.exe to your programs folder, e.g. C:\Program Files\LineUp
3. Add C:\Program Files\LineUp\ to your machine's PATH environment variable - see http://www.java.com/en/download/help/path.xml for background

There'll be an installer at some point.


-----
LICENSE

This code is mine, mine I tell you (for now, I'll decide on an open source license soon)

-----
USAGE



----
TODO

New Commands: lu revert, lu rmpth
Rewrite with tests
Add more things to the config
Init config from a file
Downloadable configs
Use lu for most common use case, e.g. "lu .net 4.0", then lusetup for config / status etc
Powershell
Test on a few other environments
Change name to _up (too geeky?, definitely, sounds good)
Help output
License
