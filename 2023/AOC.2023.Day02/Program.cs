using Day02;

var parser = new Parser("input.txt");

List<Game> games = await parser.Parse();

int answerForPart1 = games.Where(game =>
{
    return game.GameSets.All(set => set.RevealedCubes[CubeColor.Red] <= 12 &&
                                    set.RevealedCubes[CubeColor.Green] <= 13 &&
                                    set.RevealedCubes[CubeColor.Blue] <= 14);
}).Sum(x => x.GameId);

int answerForPart2 = games.Sum(game => game.CalculateMinimumPower());

Console.WriteLine($"Answer For Part 1: {answerForPart1}");
Console.WriteLine($"Answer For Part 2: {answerForPart2}");