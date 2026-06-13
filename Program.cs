Console.Write("Run Debug/Wordle (d/w): ");
string input = Console.ReadLine()!;
if (input == "d") { Debug.run(); }
if (input == "w") { Wordle.run(); }