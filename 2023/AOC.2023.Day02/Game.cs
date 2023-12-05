using System.Text;

namespace Day02;

public class Game
{
    public List<GameSet> GameSets = new();

    public Game(int gameId)
    {
        GameId = gameId;
    }

    public int GameId { get;}

    public int CalculateMinimumPower()
    {
        var maxBlue = GameSets.Max(x => x.Blue);
        var maxRed = GameSets.Max(x => x.Red);
        var maxGreen = GameSets.Max(x => x.Green);

        return maxBlue * maxRed * maxGreen;
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();

        strBuilder.Append($"Game {GameId}: ");
        foreach (var set in GameSets)
        {
            strBuilder.Append(set);
            strBuilder.Append("; ");
        }

        return strBuilder.ToString();
    }
}

public class GameSet
{
    public Dictionary<CubeColor, int> RevealedCubes { get; } = new()
    {
        { CubeColor.Blue, 0 },
        { CubeColor.Green, 0 },
        { CubeColor.Red, 0 }
    };

    public void AddToColor(CubeColor color, int count)
    {
        RevealedCubes[color] += count;
    }

    public int Blue => RevealedCubes[CubeColor.Blue];
    public int Red => RevealedCubes[CubeColor.Red];
    public int Green => RevealedCubes[CubeColor.Green];

    public override string ToString()
    {
        return $"Blue {Blue}, Red {Red}, Green {Green}";
    }
}
