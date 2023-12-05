var lines = File.ReadAllLines("input.txt");

var sum = 0;
foreach (var line in lines)
{
    var comp1 = line.Substring(0, line.Length / 2);
    var comp2 = line.Substring(line.Length / 2);

    var a = new HashSet<char>(comp1);
    var b = new HashSet<char>(comp2);

    var result = new HashSet<char>(a);
    result.IntersectWith(b);
    char diff = result.Single();
    var abc = char.IsLower(diff) ? diff - 'a' + 1 : diff - 'A' + 27;
    Console.WriteLine($"Diff {diff}: {abc}");

    sum += abc;

}

Console.WriteLine(sum);