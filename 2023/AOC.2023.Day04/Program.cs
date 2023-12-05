// See https://aka.ms/new-console-template for more information

using Day04;

var parser = new Parser("input.txt");

var cards = parser.Parse().ToDictionary(x => x.Number);

foreach (Card card in cards.Values)
{
    card.UpdateCopyCount(cards);
    Console.WriteLine(card);
}

var result1 = cards.Values.Sum(card => card.CalculateScore());


var totalCopies = cards.Values.Sum(x => x.CopyCount);
Console.WriteLine();
Console.WriteLine($"Result For Part 1: {result1}");
Console.WriteLine($"Result For Part 2: {totalCopies}");
