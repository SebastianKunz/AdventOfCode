namespace Day07;

public class Parser
{
    private readonly string _fileName;

    public Parser(string fileName)
    {
        _fileName = fileName;
    }

    public List<Hand> Parse()
    {
        var lines = File.ReadAllLines(_fileName);

        var result = new List<Hand>();
        foreach (var line in lines)
        {
            var split = line.Split(" ");
            var cards = split[0].Select(CharToCardType).ToArray();
            var bid = int.Parse(split[1]);

            var hand = new Hand(cards, bid);
            result.Add(hand);
        }

        return result;
    }

    private CardType CharToCardType(char c) => c switch
    {
        '2' => CardType.Two,
        '3' => CardType.Three,
        '4' => CardType.Four,
        '5' => CardType.Five,
        '6' => CardType.Six,
        '7' => CardType.Seven,
        '8' => CardType.Eight,
        '9' => CardType.Nine,
        'T' => CardType.Ten,
        'J' => CardType.Jack,
        'Q' => CardType.Queen,
        'K' => CardType.King,
        'A' => CardType.Ace,
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
    };
}