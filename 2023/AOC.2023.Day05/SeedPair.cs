namespace Day05;

public class SeedPair
{
    public SeedPair(ulong start, ulong range)
    {
        Start = start;
        Range = range;
    }
    public ulong Start { get; }
    
    public ulong Range { get; }

    public ulong Count => Start + Range - 1;
}