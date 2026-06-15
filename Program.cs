Dictionary<string, int> wordList = File.ReadAllLines("five-ltr-words.txt").ToDictionary(word => word, word => 0);
List<char> chars = Enumerable.Range('a', 26)
                                  .Select(i => (char)i)
                                  .ToList();

Console.Write("Run Debug/Wordle (d/w): ");
string input = Console.ReadLine()!;

if (input == "d") { Debug.run(wordList); }
if (input == "w") { Wordle.run(wordList); }
if (input == "s") { Stats.PrintStatsToFile(); }