function Fib(n) {
  return n < 2 ? n : Fib(n - 1) + Fib(n - 2);
}

function Fact(n) {
  return 1 < n ? n * Fact(n - 1) : 1;
}

var fact20, fib32, t0, elapsed;

WScript.StdOut.WriteLine("MS JScript... About to compute Fib(32) (only once)...");
WScript.StdOut.WriteLine();

t0 = new Date().valueOf();
fib32 = Fib(32);
elapsed = new Date().valueOf() - t0;

WScript.StdOut.WriteLine("Fib(32) = " + fib32.toString() + " in " + elapsed.toString() + " ms");
WScript.StdOut.WriteLine();

WScript.StdOut.WriteLine("MS JScript... About to compute 20! x 1,000,000 times...");
WScript.StdOut.WriteLine();

t0 = new Date().valueOf();
for (var i = 0; i < 1000000; i++)
{
  fact20 = Fact(20);
}
elapsed = new Date().valueOf() - t0;

WScript.StdOut.WriteLine("20! = " + fact20.toString() + " x 1,000,000 times in " + elapsed.toString() + " ms");
WScript.StdOut.WriteLine();
