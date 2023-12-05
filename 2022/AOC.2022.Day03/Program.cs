string[] lines = File.ReadAllLines("input.txt");

var sum = 0;
foreach (string line in lines)
{
    string comp1 = line.Substring(0, line.Length / 2);
    string comp2 = line.Substring(line.Length / 2);

    var a = new HashSet<char>(comp1);
    var b = new HashSet<char>(comp2);

    var result = new HashSet<char>(a);
    result.IntersectWith(b);
    char diff = result.Single();
    int abc = char.IsLower(diff) ? diff - 'a' + 1 : diff - 'A' + 27;
    Console.WriteLine($"Diff {diff}: {abc}");

    sum += abc;
}

Console.WriteLine(sum);