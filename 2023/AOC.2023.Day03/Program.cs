using Day03;

var parser = new Parser("input.txt");

EngineSchematic schematic = await parser.Parse();

int result = schematic.SchematicNumbers.Where(x => x.IsPartNumber).Sum(x => x.Number);

for (var i = 0; i < schematic.RawSchematic.Length; i++)
{
    List<SchematicSymbol> symbols = schematic.SchematicSymbols.Where(x => x.PosY == i).ToList();
    if (symbols.Count == 0)
        continue;
    Console.WriteLine($"Current idx: {i}");
    foreach (SchematicSymbol symbol in symbols)
    {
        string partNumbers = string.Join(", ", symbol.PartNumbers.Select(x => x.Number));
        Console.WriteLine($"{symbol.Symbol}: [{partNumbers}]");
    }

    Console.WriteLine($"{i - 1}: {schematic.RawSchematic[i - 1]}");
    Console.WriteLine($"{i}: {schematic.RawSchematic[i]}");
    Console.WriteLine($"{i + 1}: {schematic.RawSchematic[i + 1]}");
}

int part2Result = schematic.SchematicSymbols.Where(x => x.IsGear).Sum(symbol => symbol.PartNumbers.Aggregate(1, (x, y) => x * y.Number));

Console.WriteLine($"Answer For Part 1: {result}");
Console.WriteLine($"Answer For Part 2: {part2Result}");