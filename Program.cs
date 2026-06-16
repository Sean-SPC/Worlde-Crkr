Dictionary<string, int> wordList = File.ReadAllLines("five-ltr-words-scores.txt").ToDictionary(line => line.Split(' ')[0], line => int.Parse(line.Split(' ')[2]));
List<char> chars = Enumerable.Range('a', 26)
                                  .Select(i => (char)i)
                                  .ToList();

Console.Write("Run Debug/Wordle (d/w/s): ");
string input = Console.ReadLine()!;

if (input == "d") { Debug.run(wordList); }
if (input == "w") { Wordle.run(wordList); }
if (input == "s") { Score.Simulate(wordList); }