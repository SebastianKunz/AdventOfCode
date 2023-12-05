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
        List<long> seedNumbers = new();
        Dictionary<string, List<DestinationSourceRange>> allMaps = new();
        foreach (string line in lines)
        {
            mode = ParseLine(line, seedNumbers, mode, allMaps);
        }

        return new Almanac(seedNumbers, allMaps);
    }

    private string ParseLine(string line, List<long> seedNumbers, string mode, Dictionary<string, List<DestinationSourceRange>> allMaps)
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

        long[] rangeNumbers = line.Split(" ", StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();

        if (!allMaps.TryGetValue(mode, out var list))
        {
            list = new List<DestinationSourceRange>();
            allMaps.Add(mode, list);
        }

        list.Add(new DestinationSourceRange(rangeNumbers[0], rangeNumbers[1], rangeNumbers[2]));
        return mode;
    }

    private void ParseSeedNumbers(string line, List<long> seedNumbers)
    {
        var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        long[] theNumbers = numbers.Skip(1).Select(long.Parse).ToArray();
        if (!_parseSeedsAsRange)
        {
            seedNumbers.AddRange(theNumbers);
        }
        else
        {
            RangeLong(theNumbers[0], theNumbers[1], seedNumbers);
            RangeLong(theNumbers[2], theNumbers[3], seedNumbers);
        }
    }

    private static void RangeLong(long start, long count, List<long> seedNumbers)
    {
        for (long i = start; i < start + count; i++)
        {
            seedNumbers.Add(i);
        }
    }
}