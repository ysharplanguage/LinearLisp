from time import time

def started_time():
	return int(1000 * time())

def elapsed_time(start):
	return int(1000 * time()) - start

N = 10000000

acc = 0

def f(x):
	return acc + (10 * x)

print("Python... About to stress the for loop vs lexical scope " + str(N) + " times...")
print("(Press return)")
input()

t0 = started_time()
for i in range(0, N):
	acc = f(i)
elapsed = elapsed_time(t0)
print("acc = " + str(acc) + " in " + str(elapsed) + " ms")
print("(Press return to exit)")
input()
