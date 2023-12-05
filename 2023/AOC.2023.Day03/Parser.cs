namespace Day03;

public class Parser
{
    private readonly string _fileName;

    public Parser(string fileName)
    {
        _fileName = fileName;
    }

    public async Task<EngineSchematic> Parse()
    {
        string[] lines = await File.ReadAllLinesAsync(_fileName);

        var schematic = new EngineSchematic(lines);

        for (var y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            ParseLine(line, y, schematic);
        }

        foreach (SchematicSymbol symbol in schematic.SchematicSymbols)
        {
            List<SchematicNumber> partNumbers = schematic.SchematicNumbers.Where(schematicNumber =>
                schematicNumber.PosY >= symbol.PosY - 1 &&
                schematicNumber.PosY <= symbol.PosY + 1 && // is on correct y position (1 higher or 1 lower or same height)
                schematicNumber.IsOnX(symbol.PosX)
            ).ToList();

            partNumbers.ForEach(x => x.IsPartNumber = true);

            symbol.PartNumbers.AddRange(partNumbers);
        }

        return schematic;
    }

    private static void ParseLine(string line, int y, EngineSchematic schematic)
    {
        for (var x = 0; x < line.Length; x++)
        {
            char c = line[x];
            if (c == '.')
            {
                continue;
            }

            if (char.IsDigit(c))
            {
                x = ParseSchematicNumber(x, line, y, schematic);
            }
            else
            {
                var symbol = new SchematicSymbol(c, x, y);
                schematic.SchematicSymbols.Add(symbol);
            }
        }
    }

    private static int ParseSchematicNumber(int x, string line, int y, EngineSchematic schematic)
    {
        int startIdx = x;
        while (x < line.Length && char.IsDigit(line[x]))
        {
            x++;
        }

        string numberStr = line.Substring(startIdx, x - startIdx);
        int number = int.Parse(numberStr);

        var schematicNumber = new SchematicNumber(number, y, startIdx, startIdx + numberStr.Length - 1, false);
        schematic.SchematicNumbers.Add(schematicNumber);
        return x - 1;
    }
}