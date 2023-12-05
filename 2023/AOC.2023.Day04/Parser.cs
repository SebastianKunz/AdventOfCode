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
        var colonSplit = line.Split(":");

        var beforeColon = colonSplit[0];
        int cardId = ParseCardId(beforeColon);

        var afterColon = colonSplit[1];

        var cards = afterColon.Split("|", StringSplitOptions.TrimEntries);

        var winningCardsStr = cards[0];
        var winningCards = ParseCards(winningCardsStr);
        var yourCardsStr = cards[1];
        var yourCards = ParseCards(yourCardsStr);
        return new Card(cardId, winningCards, yourCards);
    }

    private static IReadOnlySet<int> ParseCards(string winningCards)
    {
        var cardStrs = winningCards.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        return cardStrs.Select(int.Parse).ToHashSet();
    }

    private static int ParseCardId(string beforeColon)
    {
        var cardIdStr = beforeColon.Substring("Card ".Length - 1);
        var cardId = int.Parse(cardIdStr);
        return cardId;
    }
}