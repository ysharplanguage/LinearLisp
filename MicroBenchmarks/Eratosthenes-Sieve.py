from time import time

def started_time():
	return int(1000 * time())

def elapsed_time(start):
	return int(1000 * time()) - start

thePrimes = []

def getPrimesBelow(n):
	if (n <= 2):
		return
	prime = [True] * n
	prime[0] = False
	prime[1] = False
	i = 2
	while (i * i < n):
		if (prime[i]):
			j = i
			while (j * i < n):
				prime[j * i] = False
				j = j + 1
		i = i + 1
	for i in range(0, len(prime)):
		if (prime[i]):
			thePrimes.append(i)

N = 15485865

print("Python... About to find all primes below " + str(N) + "...")
print("(Press return)")
input()

t0 = started_time()
getPrimesBelow(N)
elapsed = elapsed_time(t0)
print(str(len(thePrimes)) + " primes found in " + str(elapsed) + " ms...")
print("first... " + str(thePrimes[0]));
print("last ... " + str(thePrimes[len(thePrimes) - 1]));
print("(Press return to exit)")
input()
