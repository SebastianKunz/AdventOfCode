namespace Day05;

public class DestinationSourceRange
{
    public DestinationSourceRange(ulong destinationRangeStart, ulong sourceRangeStart, ulong rangeLength)
    {
        DestinationRangeStart = destinationRangeStart;
        SourceRangeStart = sourceRangeStart;
        RangeLength = rangeLength;
        EndOfSourceRange = SourceRangeStart + rangeLength - 1;
        EndOfDestinationRange = DestinationRangeStart + rangeLength - 1;
    }

    public ulong DestinationRangeStart { get; }
    public ulong SourceRangeStart { get; }
    public ulong RangeLength { get; }
    
    public ulong EndOfSourceRange { get; }
    public ulong EndOfDestinationRange { get; }
}