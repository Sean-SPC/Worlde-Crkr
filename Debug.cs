using System.Formats.Asn1;
using System.Text.RegularExpressions;

class Debug
{
    private static List<string> wordList = File.ReadAllLines("five-ltr-words.txt").ToList();
    // Regex For Each Character Index
    private static List<string> indexRegex = Enumerable.Repeat("[^1]", 5).ToList();
    private static string regexPattern = "";
    private static string containRegex = "";
    private static List<char> containsList = new List<char>();

    public static void run(string debugAnswer = "_0", int sleepTime = 0)
    {
        PrintTitle();

        // Getting Random Word For Answer
        if (debugAnswer == "_0") { debugAnswer = wordList[new Random().Next(0, wordList.Count)]; }
        Console.WriteLine("\nAnswer: " + debugAnswer);

        int iteration = 1;
        while (true)
        {
            Console.WriteLine("\n| Iteration " + iteration + " |");
            if (sleepTime != 0) { Thread.Sleep(sleepTime); }

            // Input Word
            string input = wordList[0];     // Use Top of Wordlist
            Console.WriteLine("\nInput: " + input);

            // Check Word Against Test Answer
            List<int> output = GetOutput(input, debugAnswer);
            Console.Write("Output: ");
            Console.WriteLine(string.Join(", ", output));

            // Check For Answer
            if (output.All(x => x == 1)) { Console.WriteLine("\nWord Found! {" + iteration + "} : " + input); break; }

            // Handle Output
            HandleOutput(input, output);

            // Construct Pattern
            regexPattern = "^";
            regexPattern += containRegex;
            foreach (string str in indexRegex) { regexPattern += str; }
            regexPattern += "$";

            // Print Regex Pattern
            Console.WriteLine("Regex: " + regexPattern);
            Console.WriteLine("Match: " + Regex.IsMatch(input, regexPattern));

            // Filter Word List
            wordList.RemoveAll(word => !Regex.IsMatch(word, regexPattern));

            iteration++;
        }
    }

    // HELPER FUNCTIONS

    static void PrintTitle()
    {
        Console.WriteLine("._.   __      __                .___.__            _________         __              ._. ");
        Console.WriteLine("| |  /  \\    /  \\___________  __| _/|  |   ____    \\_   ___ \\_______|  | _________   | | ");
        Console.WriteLine("|_|  \\   \\/\\/   /  _ \\_  __ \\/ __ | |  | _/ __ \\   /    \\  \\/\\_  __ \\  |/ /\\_  __ \\  |_| ");
        Console.WriteLine("|-|   \\        (  <_> )  | \\/ /_/ | |  |_\\  ___/   \\     \\____|  | \\/    <  |  | \\/  |-| ");
        Console.WriteLine("| |    \\__/\\  / \\____/|__|  \\____ | |____/\\___  >   \\______  /|__|  |__|_ \\ |__|     | | ");
        Console.WriteLine("|_|         \\/                   \\/           \\/           \\/            \\/          |_|    ");
    }

    static List<int> GetOutput(string input, string debugAnswer)
    {
        List<int> output = [..new int[5]];
        string answerCopy = debugAnswer;

        // Find Exact Matches
        for (int x = 0; x < 5; x++)
        {
            if (input[x] == answerCopy[x])
            {
                output[x] = 1;
                answerCopy = answerCopy.Remove(x, 1).Insert(x, "%");
            }
        }
        // Find Partial Matches & Non-Matches
        for (int x = 0; x < 5; x++)
        {
            if (output[x] != 1)
            {
                if (answerCopy.Contains(input[x]))
                {
                    output[x] = 0;
                    answerCopy = answerCopy.Remove(answerCopy.IndexOf(input[x]), 1).Insert(answerCopy.IndexOf(input[x]), "%");
                }
                else { output[x] = -1; }
            }
        }

        return output;
    }

    static void HandleOutput(string input, List<int> output)
    {
        List<char> checkedLetters = new List<char>(); // To Handle Double Letters, E.g. "e" In Input "trees" Where Output Is [-1, -1, 0, -1, -1] For Answer "plane"
        for (int x = 0; x < 5; x++)
        {
            // 1 : character in word, in right place
            if (output[x] == 1) { indexRegex[x] = input[x].ToString(); }
            // 0 : character in word, in wrong place
            if (output[x] == 0) {

                indexRegex[x] = indexRegex[x].Insert(3, input[x].ToString());
                // If Contains Regex Does Not Already Exist for Letter, Add E.g. (?=.*a)
                if (!containsList.Contains(input[x])) { containRegex += $"(?=.*{input[x]})"; }
            }
            // -1 : character not in word
            if (output[x] == -1)
            {
                // If Letter Already Checked But Is Not At Current Index, Only Mark Index
                if (checkedLetters.Contains(input[x])) { indexRegex[x] = indexRegex[x].Insert(3, input[x].ToString()); }
                // Else, If letter Not Already Checked, Mark All Indexes
                else
                {
                    for (int y = 0; y < 5; y++)
                    {
                        // Skipping Indexes With Found Letter (Regex Length = 1 E.g. "a" not "[^1a...]")
                        if (indexRegex[y].Length > 1) { indexRegex[y] = indexRegex[y].Insert(3, input[x].ToString()); }
                    }
                }
            }
            checkedLetters.Add(input[x]);
        }
    }
}