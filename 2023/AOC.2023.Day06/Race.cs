namespace Day06;

public class Race
{
    public Race(long time, long distance)
    {
        Time = time;
        Distance = distance;
    }

    public long Time { get; }
    public long Distance { get; }

    public (long, long) CalculateMinMaxHoldingTime()
    {
        // (time - holdButtonTime) * holdButtonTime >= distance
        // -holdButtonTime^2 + time * holdButtonTime - distance >= 0
        // quadratic formula = ax^2 + bx + c;
        // lets solve for holdButtonTime using quadratic equation
        const long a = -1;
        long c = -Distance;
        long b = Time;
        double root = Math.Sqrt(b * b - 4 * a * c);
        double positiveNumerator = -b + root;
        double negativeNumerator = -b - root;
        const long denominator = 2 * a;

        double min = positiveNumerator / denominator;
        double max = negativeNumerator / denominator;

        return ((long)Math.Round(min, MidpointRounding.ToEven), (long)Math.Round(max, MidpointRounding.ToZero));
    }
}