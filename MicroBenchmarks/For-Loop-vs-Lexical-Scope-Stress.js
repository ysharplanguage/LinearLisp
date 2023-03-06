var N = 10000000;

var acc = 0;

function f(x) { return acc + (10 * x); }

var acc, t0, elapsed;
WScript.StdOut.WriteLine("MS JScript... About to stress the for loop vs lexical scope " + N.toString() + " times...");
WScript.StdOut.WriteLine("(Press return)");
WScript.StdIn.ReadLine();

t0 = new Date().valueOf();
for(var i = 0; i < N; i++) acc = f(i);
elapsed = new Date().valueOf() - t0;

WScript.StdOut.WriteLine("acc = " + acc.toString() + " in " + elapsed.toString() + " ms");
WScript.StdOut.WriteLine("(Press return to exit)");
WScript.StdIn.ReadLine();
