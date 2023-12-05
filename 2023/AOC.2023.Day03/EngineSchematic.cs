namespace Day03;

public class EngineSchematic
{
    public EngineSchematic(string[] rawSchematic)
    {
        RawSchematic = rawSchematic;
    }

    public string[] RawSchematic { get; set; }

    public List<SchematicNumber> SchematicNumbers { get; } = new();

    public List<SchematicSymbol> SchematicSymbols { get; } = new();
}

public class SchematicSymbol
{
    public SchematicSymbol(char symbol, int posX, int posY)
    {
        Symbol = symbol;
        PosX = posX;
        PosY = posY;
    }

    public char Symbol { get; }
    public int PosX { get; }
    public int PosY { get; }

    public bool IsGear => Symbol == '*' && PartNumbers.Count == 2;

    public List<SchematicNumber> PartNumbers { get; } = new List<SchematicNumber>();
}

public class SchematicNumber
{
    public SchematicNumber(int number, int posY, int posXMin, int posXMax, bool isPartNumber)
    {
        PosY = posY;
        PosXMin = posXMin;
        PosXMax = posXMax;
        IsPartNumber = isPartNumber;
        Number = number;
    }

    public bool IsOnX(int x)
    {
        return PosXMax >= x - 1 && PosXMax <= x + 1 ||
               PosXMin >= x - 1 && PosXMin <= x + 1;
    }

    public int Number { get; }
    public int PosY { get; }

    public int PosXMin { get; }

    public int PosXMax { get; }

    public bool IsPartNumber { get; set; }
}