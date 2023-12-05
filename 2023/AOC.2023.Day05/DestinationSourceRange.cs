namespace Day05;

public class DestinationSourceRange
{
    public long DestinationRangeStart { get; }
    public long SourceRangeStart { get; }
    public long RangeLength { get; }

    public DestinationSourceRange(long destinationRangeStart, long sourceRangeStart, long rangeLength)
    {
        DestinationRangeStart = destinationRangeStart;
        SourceRangeStart = sourceRangeStart;
        RangeLength = rangeLength;
    }
}