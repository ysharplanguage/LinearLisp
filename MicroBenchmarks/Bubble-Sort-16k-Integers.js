function BubbleSort(array)
{
  var more = true;
  for (var i = 1; (i <= (array.length - 1)) && more; i++)
  {
    more = false;
    for (var j = 0; j < (array.length - 1); j++)
    {
      if (array[j + 1] < array[j])
      {
        var temp = array[j];
        array[j] = array[j + 1];
        array[j + 1] = temp;
        more = true;
      }
    }
  }
}

var fso = WScript.CreateObject("Scripting.FileSystemObject");
var _16kIntsFile = fso.OpenTextFile("16K-INTS.txt", 1);
var _16kIntsText = _16kIntsFile.ReadAll();
var _16kIntsList = _16kIntsText.split(",");
var _16kInts = new Array(_16kIntsList.length);
for (var i = 0; i < _16kInts.length; i++) _16kInts[i] = parseInt(_16kIntsList[i]);
_16kIntsFile.Close();

WScript.StdOut.WriteLine("MS JScript... About to bubble-sort " + _16kInts.length.toString() + " integers...")
WScript.StdOut.WriteLine("(Press return)");
WScript.StdIn.ReadLine();
var t0 = new Date().valueOf();
BubbleSort(_16kInts);
var elapsed = new Date().valueOf() - t0;
WScript.StdOut.WriteLine("sorted " + _16kInts.length.toString() + " integers in " + elapsed.toString() + " ms...");
WScript.StdOut.WriteLine("first... " + _16kInts[0].toString());
WScript.StdOut.WriteLine("last ... " + _16kInts[_16kInts.length - 1].toString());
WScript.StdOut.WriteLine("(Press return to exit)");
WScript.StdIn.ReadLine();
