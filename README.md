# Apply

This library contains an extension method that takes a function as an argument and applies it to the target value.

```cs
using Ttd2089.Apply;

var seven = 10.ApplyFn(x => x - 3);

Console.WriteLine(seven);

// output: 7
```
