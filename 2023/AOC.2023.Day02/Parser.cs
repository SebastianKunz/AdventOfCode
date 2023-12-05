﻿namespace Day02;

public class Parser
{
    private readonly string _fileName;

    public Parser(string fileName)
    {
        _fileName = fileName;
    }

    public async Task<List<Game>> Parse()
    {
        string[] lines = await File.ReadAllLinesAsync(_fileName);

        return lines.Select(ParseGameFromLine).ToList();

    }

    private Game ParseGameFromLine(string line)
    {
        // ["Game 1", "3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"]
        string[] colonSplit = line.Split(":");

        // "Game 1"
        var gamePrefix = colonSplit[0];

        var gameId = ParseGameId(gamePrefix);

        // "3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
        var turnString = colonSplit[1];
        var game = new Game(gameId);

        ParseTurns(turnString, game);

        return game;
    }

    private static void ParseTurns(string turnString, Game game)
    {
        // ["3 blue, 4 red", "1 red, 2 green, 6 blue, "2 green"]
        string[] turnSplits = turnString.Split(";");
        foreach (string turnStr in turnSplits)
        {
            ParseSingleTurn(turnStr, game);
        }
    }

    private static void ParseSingleTurn(string turnStr, Game game)
    {
        // "3 blue, 4 red" -> ["3 blue", "4 red"]
        string[] revealedCubes = turnStr.Split(",", StringSplitOptions.TrimEntries);
        var gameSet = new GameSet();
        foreach (string revealedCube in revealedCubes)
        {
            ParseRevealedCube(revealedCube, gameSet);
        }

        game.GameSets.Add(gameSet);
    }

    private static void ParseRevealedCube(string revealedCube, GameSet gameSet)
    {
        // "3 blue" -> ["3", "blue"]
        var revealedCubeSplit = revealedCube.Split(" ");

        var revealedCubeCountStr = revealedCubeSplit[0];
        var revealedCubeColorStr = revealedCubeSplit[1];
        var revealedCubeColor = Enum.Parse<CubeColor>(revealedCubeColorStr, true);
        var revealedCubeCount = int.Parse(revealedCubeCountStr);

        gameSet.AddToColor(revealedCubeColor, revealedCubeCount);
    }

    private static int ParseGameId(string gamePrefix)
    {
        // Just the game id, since the length of "Game" is static.
        var gameIdStr = gamePrefix.Substring("Game".Length);
        var gameId = int.Parse(gameIdStr);
        return gameId;
    }
}