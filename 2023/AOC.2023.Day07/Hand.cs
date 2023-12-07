namespace Day07;

public class Hand : IComparable<Hand>
{
    public CardType[] Cards { get; }
    public int Bid { get; }

    public HandType HandType { get; }

    public Hand(CardType[] cards, int bid)
    {
        Cards = cards;
        Bid = bid;
        CardWeight = Cards.GroupBy(x => x)
            .OrderByDescending(x => x.Count())
            .ToDictionary(grouping => grouping.Key, grouping => grouping.Count());
        HandType = CalculateHandType();
    }

    public Dictionary<CardType, int> CardWeight { get; }

    private HandType CalculateHandType()
    {
        if (CardWeight.Count == 1)
        {
            return HandType.FiveOfAKind;
        }

        var first = CardWeight.First();
        var second = CardWeight.Skip(1).First();
        if (CardWeight.Count == 2)
        {
            if (first.Value == 4 && second.Value == 1)
                return HandType.FourOfAKind;

            if (first.Value == 3 && second.Value == 2)
                return HandType.FullHouse;
        }

        if (first.Value == 3)
        {
            return HandType.ThreeOfAKind;
        }

        if (first.Value == 2 && second.Value == 2)
        {
            return HandType.TwoPair;
        }

        if (first.Value == 2 && second.Value == 1)
        {
            return HandType.OnePair;
        }

        if (first.Value == 1)
        {
            return HandType.HighCard;
        }

        throw new Exception();
    }

    public int CompareTo(Hand? other)
    {
        if (other.HandType != HandType)
            return HandType - other.HandType;

        for (int i = 0; i < Cards.Length; i++)
        {
            var cardType = Cards[i];
            var otherCardType = other.Cards[i];

            if (cardType != otherCardType)
            {
                return cardType - otherCardType;
            }
        }

        return 0;
    }

    public override string ToString()
    {
        var cardsAsStr = string.Join("", Cards.Select(CardTypeToString));
        var cardsSortedAsStr = string.Join("", CardWeight.Select(kvp => new string(CardTypeToString(kvp.Key), kvp.Value)));

        return $"{cardsAsStr} ({cardsSortedAsStr}) {Bid,3} | {HandType}";
    }

    public char CardTypeToString(CardType t) => t switch
    {
        CardType.Two => '2',
        CardType.Three => '3',
        CardType.Four => '4',
        CardType.Five => '5',
        CardType.Six => '6',
        CardType.Seven => '7',
        CardType.Eight => '8',
        CardType.Nine => '9',
        CardType.Ten => 'T',
        CardType.Jack => 'J',
        CardType.Queen => 'Q',
        CardType.King => 'K',
        CardType.Ace => 'A',
        _ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
    };
}