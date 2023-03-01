function f(x) { return 10 * x; }

var acc, t0, elapsed;
WScript.StdOut.WriteLine("MS JScript... About to stress the for loop vs lexical scope 10,000,000 times...");
WScript.StdOut.WriteLine("(Press return)");
WScript.StdIn.ReadLine();

t0 = new Date().valueOf();
var acc = 0;
for(var i = 0; i < 10000000; i++) acc += f(i);
elapsed = new Date().valueOf() - t0;

WScript.StdOut.WriteLine("acc = " + acc.toString() + " in " + elapsed.toString() + " ms");
WScript.StdOut.WriteLine();
