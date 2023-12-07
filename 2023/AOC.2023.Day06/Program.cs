using Day06;

var parse = new Parser("input.txt");

List<Race> races = parse.Parse();

var result = 1L;
foreach (Race race in races)
{
   (long, long) holdButtonTime = race.CalculateMinMaxHoldingTime();

   long possibleSolutions = holdButtonTime.Item2 - holdButtonTime.Item1;
   result *= possibleSolutions;
   
   Console.WriteLine(holdButtonTime);
   Console.WriteLine($"Possible Solutions: {possibleSolutions}");
}

Console.WriteLine(result);
