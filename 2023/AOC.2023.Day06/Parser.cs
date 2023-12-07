namespace Day06;

public class Parser
{
    private readonly string _fileName;

    public Parser(string fileName)
    {
        _fileName = fileName;
    }

    public List<Race> Parse(bool isPart2 = true)
    {
        var lines = File.ReadAllLines(_fileName);


        var timeLine = lines.First();
        var distanceLine = lines.Last();
        var times = ParseLine(timeLine, isPart2);
        var distances = ParseLine(distanceLine, isPart2);

        List<Race> races = new();

        for (var index = 0; index < times.Length; index++)
        {
            long time = times[index];
            long distance = distances[index];

            races.Add(new Race(time, distance));
        }

        return races;
    }

    private static long[] ParseLine(string inputLine, bool isPart2)
    {
        var idx = inputLine.IndexOf(":", StringComparison.Ordinal);
        inputLine = inputLine.Substring(idx + 1);
        if (isPart2)
        {
            inputLine = inputLine.Replace(" ", "");
        }
        var times = inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        return times;
    }
}