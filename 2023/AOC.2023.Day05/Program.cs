using Day05;
using System.Diagnostics;

var sw = Stopwatch.StartNew();

var parser = new Parser("input.txt");

Almanac almanac = parser.Parse();
sw.Stop();
Console.WriteLine($"Parsing took {sw.ElapsedMilliseconds}ms");
sw.Restart();

long result = almanac.FindLowestLocationNumber();

sw.Stop();
Console.WriteLine($"Solving took in {sw.ElapsedMilliseconds}ms ({sw.ElapsedMilliseconds / 1000.0:0:0.##}s)");
Console.WriteLine($"Result For Part 1: {result}");