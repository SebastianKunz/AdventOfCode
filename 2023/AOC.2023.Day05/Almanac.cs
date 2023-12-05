namespace Day05;

public class Almanac
{
    public Almanac(List<SeedPair> seeds, Dictionary<string, List<DestinationSourceRange>> allMaps)
    {
        foreach (List<DestinationSourceRange> val in allMaps.Values)
        {
            val.Sort((a, b) => a.SourceRangeStart.CompareTo(b.SourceRangeStart));
        }

        _seedToSoil = allMaps["seed-to-soil"];
        _soilToFertilizer = allMaps["soil-to-fertilizer"];
        _fertilizerToWater = allMaps["fertilizer-to-water"];
        _waterToLight = allMaps["water-to-light"];
        _lightToTemperature = allMaps["light-to-temperature"];
        _temperatureToHumidity = allMaps["temperature-to-humidity"];
        _humidityToLocation = allMaps["humidity-to-location"];

        Seeds = seeds;
    }

    private List<SeedPair> Seeds { get; }

    private readonly List<DestinationSourceRange> _seedToSoil;
    private readonly List<DestinationSourceRange> _soilToFertilizer;
    private readonly List<DestinationSourceRange> _fertilizerToWater;
    private readonly List<DestinationSourceRange> _waterToLight;
    private readonly List<DestinationSourceRange> _lightToTemperature;
    private readonly List<DestinationSourceRange> _temperatureToHumidity;
    private readonly List<DestinationSourceRange> _humidityToLocation;

    private ulong FindSource(ulong number, List<DestinationSourceRange> map)
    {
        DestinationSourceRange? range = FindClosestRange(number, map);

        var mapping = CalcDestination(number, range);

        if (mapping.HasValue)
        {
            return mapping.Value;
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

    private ulong CalculateLocationFor(ulong seedNumber)
    {
        ulong soilNumber = FindSource(seedNumber, _seedToSoil);
        ulong fertilizerNumber = FindSource(soilNumber, _soilToFertilizer);
        ulong waterNumber = FindSource(fertilizerNumber, _fertilizerToWater);
        ulong lightNumber = FindSource(waterNumber, _waterToLight);
        ulong tempNumber = FindSource(lightNumber, _lightToTemperature);
        ulong humidityNumber = FindSource(tempNumber, _temperatureToHumidity);
        ulong locationNumber = FindSource(humidityNumber, _humidityToLocation);
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