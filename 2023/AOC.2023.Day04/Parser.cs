namespace Day04;

public class Parser
{
    private readonly string _fileName;

    public Parser(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<Card> Parse()
    {
        string[] lines = File.ReadAllLines(_fileName);

        return lines.Select(ParseGame);
    }

    private static Card ParseGame(string line)
    {
        string[] colonSplit = line.Split(":");

        string beforeColon = colonSplit[0];
        int cardId = ParseCardId(beforeColon);

        string afterColon = colonSplit[1];

        string[] cards = afterColon.Split("|", StringSplitOptions.TrimEntries);

        string winningCardsStr = cards[0];
        IReadOnlySet<int> winningCards = ParseCards(winningCardsStr);
        string yourCardsStr = cards[1];
        IReadOnlySet<int> yourCards = ParseCards(yourCardsStr);
        return new Card(cardId, winningCards, yourCards);
    }

    private static IReadOnlySet<int> ParseCards(string winningCards)
    {
        string[] cardStrs = winningCards.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        return cardStrs.Select(int.Parse).ToHashSet();
    }

    private static int ParseCardId(string beforeColon)
    {
        string cardIdStr = beforeColon.Substring("Card ".Length - 1);
        int cardId = int.Parse(cardIdStr);
        return cardId;
    }
}