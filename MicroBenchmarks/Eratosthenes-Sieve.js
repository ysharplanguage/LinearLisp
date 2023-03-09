var thePrimes = [];

function getPrimesBelow(n)
{
  if (n <= 2) return;
  var prime = new Array(n);
  for (var i = 2; i < n; i++) prime[i] = true;
  prime[1] = prime[0] = false;
  for (var i = 2; i * i < n; i++)
  {
    if (prime[i]) for (var j = i; j * i < n; j++) prime[j * i] = false;
  }
  for (var i = 0; i < prime.length; i++) if (prime[i]) thePrimes.push(i);
}

var N = 15485865;

WScript.StdOut.WriteLine("MS JScript... About to find all primes below " + N.toString() + "...")
WScript.StdOut.WriteLine("(Press return)");
WScript.StdIn.ReadLine();
var t0 = new Date().valueOf();
getPrimesBelow(N);
var elapsed = new Date().valueOf() - t0;
WScript.StdOut.WriteLine(thePrimes.length.toString() + " primes found in " + elapsed.toString() + " ms...");
WScript.StdOut.WriteLine("first... " + thePrimes[0].toString());
WScript.StdOut.WriteLine("last ... " + thePrimes[thePrimes.length - 1].toString());
WScript.StdOut.WriteLine("(Press return to exit)");
WScript.StdIn.ReadLine();
