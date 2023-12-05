namespace Day04;

public class Card
{
    public Card(int number, IReadOnlySet<int> winningNumbers, IReadOnlySet<int> yourNumbers)
    {
        Number = number;
        WinningNumbers = winningNumbers;
        YourNumbers = yourNumbers;
        var overlapping = new HashSet<int>(winningNumbers);
        overlapping.IntersectWith(yourNumbers);
        Overlapping = overlapping;
        IsOriginal = true;
        CopyCount = 1;
    }

    public int Number { get; }

    public IReadOnlySet<int> WinningNumbers { get; }
    public IReadOnlySet<int> YourNumbers { get; }

    public IReadOnlySet<int> Overlapping { get; }

    public int CopyCount { get; private set; }

    public bool IsOriginal { get; }

    public int CalculateScore()
    {
        if (Overlapping.Count <= 1)
            return Overlapping.Count;

        return (int)Math.Pow(2, Overlapping.Count - 1);
    }

    public override string ToString()
    {
        string formattedNumber = $"Card {Number,3}";
        string formattedScore = $"Score: {CalculateScore(),3}";
        string formattedCopyCount = $"CopyCount: {CopyCount,6}";
        string winningNumbers = string.Join(" ", WinningNumbers.Select(n => $"{n,2}"));
        string yourNumbers = string.Join(" ", YourNumbers.Select(n => $"{n,2}"));

        return $"{formattedNumber} [{formattedScore} {formattedCopyCount}]: {winningNumbers} | {yourNumbers}";
    }

    public void UpdateCopyCount(Dictionary<int, Card> cards)
    {
        int matching = Overlapping.Count;
        for (int i = 1; i <= matching; i++)
        {
            Card copiedCard = cards[Number + i];
            copiedCard.CopyCount += CopyCount;
        }
    }
}