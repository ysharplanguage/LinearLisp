from time import time

def started_time():
	return int(1000 * time())
def elapsed_time(start):
	return int(1000 * time()) - start
def Fib(n):
    if n < 2:
        return n
    return Fib(n - 1) + Fib(n - 2)
def Fact(n):
    if 1 < n:
        return n * Fact(n - 1)
    return 1

print("Python... About to compute Fib(32) (only once)...")
print("(Press return)")
input()

t0 = started_time()
fib32 = Fib(32)
elapsed = elapsed_time(t0)
print("Fib(32) = " + str(fib32) + " in " + str(elapsed) + " ms")

print()
print("Python... About to compute 20! x 1,000,000 times...")
print("(Press return)")
input()

t0 = started_time()
for i in range(0, 1000000):
	fact20 = Fact(20)
elapsed = elapsed_time(t0)
print("20! = " + str(fact20) + " x 1,000,000 times in " + str(elapsed) + " ms")
print("(Press return to exit)")
input()
