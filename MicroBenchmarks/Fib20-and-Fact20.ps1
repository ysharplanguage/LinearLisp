function Fib([long]$n) {
  if ($n -lt 2) {
    $n
  }
  else {
    (Fib ($n - 1)) + (Fib ($n - 2))
  }
}

function Fact([long]$n) {
  if (1 -lt $n) { $n * (Fact ($n - 1)) } else { 1 }
}

Write-Host "PowerShell... About to compute Fib(20) (only once)..."
Write-Host "(Press return)"
[System.Console]::ReadLine()

$sw = [System.Diagnostics.StopWatch]::StartNew()
$fib20 = Fib(20)
$sw.Stop()
$elapsed = $sw.ElapsedMilliseconds
Write-Host "Fib(20) = $fib20 in $elapsed ms"

Write-Host ""
Write-Host "PowerShell... About to compute 20! x 1,000 times..."
Write-Host "(Press return)"
[System.Console]::ReadLine()

$sw = [System.Diagnostics.StopWatch]::StartNew()

for ($i = 0; $i -lt 1000; $i++)
{
  $fact20 = Fact(20)
}

$sw.Stop()
$elapsed = $sw.ElapsedMilliseconds
Write-Host "20! = $fact20 in $elapsed ms"
Write-Host ""
