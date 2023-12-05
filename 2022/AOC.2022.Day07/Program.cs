using AdventOfCode;
using AdventOfCode.Extensions;

AdventDirectory root = await InputParser.Parse("input.txt");

int result = root.Dirs.Values.SelectRecursive(x => x.Dirs.Values)
    .Where(x => x.Size <= 100000)
    .Sum(x => x.Size);


Console.WriteLine(result);