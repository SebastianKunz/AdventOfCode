using System.Text.RegularExpressions;

const bool isPart1 = false;
var lookUp = new Dictionary<string, int>
{
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 }
};

int ConvertStrToInt(string str)
{
    if (int.TryParse(str, out int number)) return number;

    return lookUp[str];
}


string[] lines = await File.ReadAllLinesAsync("input.txt");

Regex regex;
if (isPart1)
    regex = new Regex("[0-9]");
regex = new Regex("(?=([0-9]|one|two|three|four|five|six|seven|eight|nine))");

List<string> MatchForPart2(MatchCollection matchCollection)
{
    var results = new List<string>();

    foreach (Match match in matchCollection)
        // Using match.Index + 1 because the lookahead matches the position before the word/digit
        if (match.Groups.Count == 2)
        {
            string wordOrDigit = match.Groups[1].Value;
            results.Add(wordOrDigit);
        }

    return results;
}

List<string> MatchForPart1(MatchCollection matchCollection)
{
    return matchCollection.Select(x => x.Value).ToList();
}

List<List<string>> list = lines.Select(input =>
{
    MatchCollection matches = regex.Matches(input);

    return isPart1 ? MatchForPart1(matches) : MatchForPart2(matches);
}).ToList();

for (var index = 0; index < list.Count; index++)
{
    List<string>? l = list[index];
    string? line = lines[index];
    Console.WriteLine($"{index}. {line}: [{string.Join(", ", l)}]");
}

int resultSum = list.Sum(l =>
{
    if (l.Count == 0)
        return 0;
    if (l.Count == 1)
    {
        int number = ConvertStrToInt(l.First());
        return number * 10 + number;
    }

    string first = l.First();
    string last = l.Last();

    int nb = ConvertStrToInt(first) * 10 + ConvertStrToInt(last);
    return nb;
});


// int answer = GetSummedResult(lines);
Console.WriteLine(resultSum);