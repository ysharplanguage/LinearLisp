@echo off

cscript //nologo For-Loop-vs-Lexical-Scope-Stress.js

cscript //nologo Fib32-and-Fact20.js

cscript //nologo Eratosthenes-Sieve.js

rem cscript //nologo Bubble-Sort-16k-Integers.js

For-Loop-vs-Lexical-Scope-Stress.py

Fib32-and-Fact20.py

Eratosthenes-Sieve.py

dotnet ..\LinearLisp\bin\Debug\net6.0\LinearLisp.dll

powershell .\For-Loop-vs-Lexical-Scope-Stress.ps1

powershell .\Fib20-and-Fact20.ps1
