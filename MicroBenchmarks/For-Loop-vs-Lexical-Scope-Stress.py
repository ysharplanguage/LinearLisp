from time import time

def started_time():
	return int(1000 * time())
def elapsed_time(start):
	return int(1000 * time()) - start

def f(x):
	return 10 * x

print("Python... About to stress the for loop vs lexical scope 10,000,000 times...")
print("(Press return)")
input()

t0 = started_time()
acc = 0
for i in range(0, 10000000):
	acc = acc + f(i)
elapsed = elapsed_time(t0)
print("acc = " + str(acc) + " in " + str(elapsed) + " ms")
print()
