function f([int]$x) {
  (10 * $x)
}

Write-Host "PowerShell... About to stress the for loop vs lexical scope 1,000,000 times..."
Write-Host ""

$acc = 0

$sw = [System.Diagnostics.StopWatch]::StartNew()

for ($i = 0; $i -lt 1000000; $i++)
{
  $acc = ($acc + (f $i))
}

$sw.Stop()
$elapsed = $sw.ElapsedMilliseconds
Write-Host "acc = $acc in $elapsed ms"
Write-Host ""
