PlayType StringToPlayType(string str)
{
    switch (str)
    {
        case "A":
        case "X":
            return PlayType.Rock;
        case "B":
        case "Y":
            return PlayType.Paper;
        case "C":
        case "Z":
            return PlayType.Scissors;
    }

    throw new ArgumentOutOfRangeException(nameof(str));
}

GameResult StringToGameResult(string str)
{
    switch (str)
    {
        case "X":
            return GameResult.Loose;
        case "Y":
            return GameResult.Draw;
        case "Z":
            return GameResult.Win;
    }

    throw new ArgumentOutOfRangeException(nameof(str));
}

var lines = File.ReadAllLines("input.txt");

var totalScore = 0;
foreach (var line in lines)
{
    var split = line.Split(" ");

    var opponent = StringToPlayType(split[0]);
    var expectedResult = StringToGameResult(split[1]);

    var whatINeedToPlay = PlayType.Paper;

    if (expectedResult == GameResult.Draw)
        whatINeedToPlay = opponent;
    else if (expectedResult == GameResult.Win)
    {
        if (opponent == PlayType.Paper)
        {
            whatINeedToPlay = PlayType.Scissors;
        }
        else if (opponent == PlayType.Rock)
        {
            whatINeedToPlay = PlayType.Paper;
        }
        else if (opponent == PlayType.Scissors)
        {
            whatINeedToPlay = PlayType.Rock;
        }
    }
    else
    {
        if (opponent == PlayType.Paper)
        {
            whatINeedToPlay = PlayType.Rock;
        }
        else if (opponent == PlayType.Rock)
        {
            whatINeedToPlay = PlayType.Scissors;
        }
        else if (opponent == PlayType.Scissors)
        {
            whatINeedToPlay = PlayType.Paper;
        }
    }
    
    totalScore+= CalcScore(whatINeedToPlay, opponent);
}

Console.WriteLine($"Result: {totalScore}");

int CalcScore(PlayType me, PlayType opponent)
{
    int score = (int)me;
    if (me == PlayType.Rock && opponent == PlayType.Scissors ||
        me == PlayType.Paper && opponent == PlayType.Rock ||
        me == PlayType.Scissors && opponent == PlayType.Paper)
    {
        score += 6;
    }
    else if (me == opponent)
    {
        score += 3;
    }

    return score;
}