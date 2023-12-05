namespace Day05;

public class Parser
{
    private readonly string _filename;
    private readonly bool _parseSeedsAsRange;

    public Parser(string filename, bool parseSeedsAsRange = true)
    {
        _filename = filename;
        _parseSeedsAsRange = parseSeedsAsRange;
    }

    public Almanac Parse()
    {
        string[] lines = File.ReadAllLines(_filename);

        var mode = "";
        List<SeedPair> seedNumbers = new();
        Dictionary<string, List<DestinationSourceRange>> allMaps = new();
        foreach (string line in lines)
        {
            mode = ParseLine(line, seedNumbers, mode, allMaps);
        }

        return new Almanac(seedNumbers, allMaps);
    }

    private string ParseLine(string line, List<SeedPair> seedNumbers, string mode, Dictionary<string, List<DestinationSourceRange>> allMaps)
    {
        if (string.IsNullOrEmpty(line))
        {
            return mode;
        }

        if (line.StartsWith("seeds"))
        {
            ParseSeedNumbers(line, seedNumbers);
            return string.Empty;
        }

        if (line.Contains("-"))
        {
            mode = line.Split(" ")[0];
            return mode;
        }

        ulong[] rangeNumbers = line.Split(" ", StringSplitOptions.TrimEntries).Select(ulong.Parse).ToArray();

        if (!allMaps.TryGetValue(mode, out List<DestinationSourceRange>? list))
        {
            list = new List<DestinationSourceRange>();
            allMaps.Add(mode, list);
        }

        list.Add(new DestinationSourceRange(rangeNumbers[0], rangeNumbers[1], rangeNumbers[2]));
        return mode;
    }

    private void ParseSeedNumbers(string line, List<SeedPair> seedNumbers)
    {
        string[] numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        ulong[] theNumbers = numbers.Skip(1).Select(ulong.Parse).ToArray();
        if (!_parseSeedsAsRange)
        {
            seedNumbers.AddRange(theNumbers.Select(start => new SeedPair(start, 0)));
        }
        else
        {
            for (int i = 0; i < theNumbers.Length; i++)
            {
                var pair = new SeedPair(theNumbers[i], theNumbers[i + 1]);
                seedNumbers.Add(pair);
                i++;
            }
        }
    }
}