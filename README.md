# LinearLisp
A proof of concept in full linearization for AST-walking interpreters

### (For .NET 6)
Compile with optimizations turned on to get run time performance in the same ballpark as Python 3.10 as of Feb. 2023

(and 3 to 5 times faster than Microsoft's JScript)

![LinearLisp vs Python vs MS JScript](https://user-images.githubusercontent.com/1055314/221786705-3559b9bb-1465-4935-be1b-ee6b06c36ec1.jpg)

Added a few basic microbenchmarks for the common / obvious contenders that come to mind:
* Microsoft's PowerShell
* Microsoft's JScript
* Python 3.1x

PS.
The Microsoft's PowerShell interpreter displays terrible performance on these microbenchmarks - it's clearly not meant to be a general purpose PL that you'd want to use for programs with calculus-heavy algorithms (unless due time is never an issue for you...)
