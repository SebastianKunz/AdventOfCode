namespace Day05;

public class Almanac
{
    private readonly Dictionary<string, List<DestinationSourceRange>> _allMaps;

    public Almanac(List<long> seeds, Dictionary<string, List<DestinationSourceRange>> allMaps)
    {
        _allMaps = allMaps;
        Seeds = seeds;
    }

    public IReadOnlyList<long> Seeds { get; }

    public IReadOnlyList<DestinationSourceRange> SeedToSoil => _allMaps["seed-to-soil"];
    public IReadOnlyList<DestinationSourceRange> SoilToFertilizer => _allMaps["soil-to-fertilizer"];
    public IReadOnlyList<DestinationSourceRange> FertilizerToWater => _allMaps["fertilizer-to-water"];
    public IReadOnlyList<DestinationSourceRange> WaterToLight => _allMaps["water-to-light"];
    public IReadOnlyList<DestinationSourceRange> LightToTemperature => _allMaps["light-to-temperature"];
    public IReadOnlyList<DestinationSourceRange> TemperatureToHumidity => _allMaps["temperature-to-humidity"];
    public IReadOnlyList<DestinationSourceRange> HumidityToLocation => _allMaps["humidity-to-location"];

    public long FindSource(long number, IReadOnlyList<DestinationSourceRange> map)
    {
        foreach (var range in map)
        {
            if (number >= range.SourceRangeStart && number <= range.SourceRangeStart + range.RangeLength - 1)
            {
                return range.DestinationRangeStart + number - range.SourceRangeStart;
            }
        }

        return number;
    }

    public long CalculateLocationFor(long seedNumber)
    {
        var soilNumber = FindSource(seedNumber, SeedToSoil);
        var fertilizerNumber = FindSource(soilNumber, SoilToFertilizer);
        var waterNumber = FindSource(fertilizerNumber, FertilizerToWater);
        var lightNumber = FindSource(waterNumber, WaterToLight);
        var tempNumber = FindSource(lightNumber, LightToTemperature);
        var humidityNumber = FindSource(tempNumber, TemperatureToHumidity);
        var locationNumber = FindSource(humidityNumber, HumidityToLocation);
        return locationNumber;
    }

    public long FindLowestLocationNumber()
    {
        Console.WriteLine($"We have to solve {Seeds.Count} seeds.");
        var min = long.MaxValue;
        for (var index = 0; index < Seeds.Count; index++)
        {
            long seedNumber = Seeds[index];
            var location = CalculateLocationFor(seedNumber);
            if (location < min)
                min = location;
            if (index % (Seeds.Count / 100) == 0)
            {
                var percent = (index / (double)Seeds.Count).ToString("0.00%");
                Console.WriteLine($"Solved {index:n0} / {Seeds.Count:n0} ({percent})");
            }
        }

        return min;
    }
}