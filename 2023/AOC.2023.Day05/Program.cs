
using Day05;
using System.Diagnostics;

Stopwatch sw = Stopwatch.StartNew();

var parser = new Parser("input.txt");

Almanac almanac = parser.Parse();
sw.Stop();
Console.WriteLine($"Parsing took {sw.ElapsedMilliseconds}ms");
sw.Restart();

var result = almanac.FindLowestLocationNumber();

sw.Stop();
Console.WriteLine($"Solving took in {sw.ElapsedMilliseconds}ms");
Console.WriteLine($"Result For Part 1: {result}");

