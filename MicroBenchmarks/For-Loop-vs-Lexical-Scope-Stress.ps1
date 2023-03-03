$N = 10000

$acc = 0

function f([int]$x) {
  $acc + (10 * $x)
}

Write-Host "PowerShell... About to stress the for loop vs lexical scope $N times..."
Write-Host "(Press return)"
[System.Console]::ReadLine()

$sw = [System.Diagnostics.StopWatch]::StartNew()

for ($i = 0; $i -lt $N; $i++)
{
  $acc = (f $i)
}

$sw.Stop()
$elapsed = $sw.ElapsedMilliseconds
Write-Host "acc = $acc in $elapsed ms"
Write-Host ""