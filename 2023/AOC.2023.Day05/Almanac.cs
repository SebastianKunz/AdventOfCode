namespace Day05;

public class Almanac
{
    private readonly Dictionary<string, List<DestinationSourceRange>> _allMaps;

    private readonly Dictionary<string, DestinationSourceRange?> _rangeCache = new();

    public Almanac(List<SeedPair> seeds, Dictionary<string, List<DestinationSourceRange>> allMaps)
    {
        foreach (List<DestinationSourceRange> val in allMaps.Values)
        {
            val.Sort((a, b) => a.SourceRangeStart.CompareTo(b.SourceRangeStart));
        }

        _rangeCache = new Dictionary<string, DestinationSourceRange?>();
        _rangeCache.Add("seed-to-soil", null);
        _rangeCache.Add("soil-to-fertilizer", null);
        _rangeCache.Add("fertilizer-to-water", null);
        _rangeCache.Add("water-to-light", null);
        _rangeCache.Add("light-to-temperature", null);
        _rangeCache.Add("temperature-to-humidity", null);
        _rangeCache.Add("humidity-to-location", null);

        _allMaps = allMaps;

        Seeds = seeds;
    }

    private List<SeedPair> Seeds { get; }

    private List<DestinationSourceRange> SeedToSoil => _allMaps["seed-to-soil"];
    private List<DestinationSourceRange> SoilToFertilizer => _allMaps["soil-to-fertilizer"];
    private List<DestinationSourceRange> FertilizerToWater => _allMaps["fertilizer-to-water"];
    private List<DestinationSourceRange> WaterToLight => _allMaps["water-to-light"];
    private List<DestinationSourceRange> LightToTemperature => _allMaps["light-to-temperature"];
    private List<DestinationSourceRange> TemperatureToHumidity => _allMaps["temperature-to-humidity"];
    private List<DestinationSourceRange> HumidityToLocation => _allMaps["humidity-to-location"];

    public ulong FindSource(ulong number, string mode, List<DestinationSourceRange> map)
    {
        var rangeHint = _rangeCache[mode];
        var cachedRange = CalcDestination(number, rangeHint);
        if (cachedRange.HasValue)
        {
            return cachedRange.Value;
        }

        DestinationSourceRange? range = FindClosestRange(number, map);
        _rangeCache[mode] = range;

        cachedRange = CalcDestination(number, range);

        if (cachedRange.HasValue)
        {
            return cachedRange.Value;
        }

        return number;
    }

    private static ulong? CalcDestination(ulong number, DestinationSourceRange? range)
    {
        if (range is not null && number >= range.SourceRangeStart && number <= range.SourceRangeStart + range.RangeLength - 1)
        {
            return range.DestinationRangeStart + number - range.SourceRangeStart;
        }

        return null;
    }

    private static DestinationSourceRange? FindClosestRange(ulong number, List<DestinationSourceRange> map)
    {
        int left = 0;
        int right = map.Count - 1;
        int mid;
        int resultIndex = -1;

        if (number < map[0].SourceRangeStart || number > map[^1].EndOfSourceRange)
        {
            return null;
        }

        while (left <= right)
        {
            mid = left + (right - left) / 2;
            var range = map[mid];

            if (number < range.SourceRangeStart)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
                resultIndex = mid;
            }
        }

        if (resultIndex == -1)
            return null;

        return map[resultIndex];
    }

    public ulong CalculateLocationFor(ulong seedNumber)
    {
        ulong soilNumber = FindSource(seedNumber, "seed-to-soil", SeedToSoil);
        ulong fertilizerNumber = FindSource(soilNumber,"soil-to-fertilizer", SoilToFertilizer);
        ulong waterNumber = FindSource(fertilizerNumber,"fertilizer-to-water", FertilizerToWater);
        ulong lightNumber = FindSource(waterNumber,"water-to-light", WaterToLight);
        ulong tempNumber = FindSource(lightNumber,"light-to-temperature", LightToTemperature);
        ulong humidityNumber = FindSource(tempNumber,"temperature-to-humidity", TemperatureToHumidity);
        ulong locationNumber = FindSource(humidityNumber, "humidity-to-location", HumidityToLocation);
        return locationNumber;
    }

    public ulong FindLowestLocationNumber()
    {
        long seedCount = Seeds.Sum(x => (long)x.Range);
        Console.WriteLine($"We have to solve {seedCount:n0} seeds.");
        var min = ulong.MaxValue;
        long count = 0;
        for (var index = 0; index < Seeds.Count; index++)
        {
            var seedPair = Seeds[index];
            for (ulong seedNumber = seedPair.Start; seedNumber < seedPair.Count; seedNumber++)
            {
                ulong location = CalculateLocationFor(seedNumber);
                if (location < min)
                {
                    min = location;
                }

                if (count % (seedCount / 100) == 0)
                {
                    var percent = (count / (double)seedCount).ToString("0.00%");
                    Console.WriteLine($"Solved {count:n0} / {seedCount:n0} ({percent})");
                }

                count++;
            }
        }

        return min;
    }
}