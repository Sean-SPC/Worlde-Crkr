using System.Formats.Asn1;
using System.Text.RegularExpressions;

class Wordle
{
    // Regex For Each Character Index
    private static List<string> indexRegex = Enumerable.Repeat("[^1]", 5).ToList();
    private static string regexPattern = "";
    private static string containRegex = "";

    public static void run(Dictionary<string, int> wordList)
    {
        PrintTitle();

        int iteration = 1;
        while (true)
        {
            Console.WriteLine("\n| Iteration " + iteration + " |\n");

            // Recommend Word
            Console.WriteLine("Recommendation: " + wordList.First().Key);

            // Input Word
            string input = GetUserInput();

            // Get Output
            List<int> output = GetOutput();

            // Check For Answer
            if (output.All(x => x == 1)) { Console.WriteLine("\nWord Found! {" + iteration + "} : " + input); break; }

            // Handle Output
            HandleOutput(input, output);

            // Construct Pattern
            regexPattern = "^";
            regexPattern += containRegex;
            foreach (string str in indexRegex) { regexPattern += str; }
            regexPattern += "$";

            // Filter Word Dictionary
            wordList = wordList
                .Where(x => Regex.IsMatch(x.Key, regexPattern))
                .ToDictionary(x => x.Key, x => x.Value);

            iteration++;

            // Simulate Word List For Best Score
            wordList = Score.Simulate(wordList);
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

    static string GetUserInput()
    {
        string str = "";
        Boolean valid = false;
        do 
        {
            Console.Write("Input: ");
            try
            {
                str = Console.ReadLine()!.ToLower();
                if (str.Any(char.IsDigit)) { throw new Exception("Invalid String: Contains Number"); }
                if (str.Length != 5) { throw new Exception("Invalid String: Not 5 Characters"); }
                valid = true;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        while (!valid);

        return str;
    }

    static List<int> GetOutput()
    {
        List<int> output = [..new int[5]];
        Boolean valid = false;
        do 
        {
            Console.Write("Output: ");
            try
            {
                output = Console.ReadLine()!
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
                foreach (int x in output)
                {
                    if (x != 1 && x != 0 && x != -1) { throw new Exception("Invalid Output: " + x + " is not of possible values : [1, 0 , -1]"); }
                }
                valid = true;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        while (!valid);

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
                if(!indexRegex.Contains(input[x].ToString())) { indexRegex[x] = indexRegex[x].Insert(3, input[x].ToString()); }
                int i = containRegex.IndexOf(input[x]);
                // If Contains Regex Does Not Already Exist for Letter, Add, E.g. (?=.*a)
                if (i != -1) 
                { 
                    if (checkedLetters.Contains(input[x])) { containRegex = containRegex.Insert(i+1, ".*" + input[x]); }
                }
                // Else, Append to Double, E.g. (?=.*a.*a)
                else { containRegex += $"(?=.*{input[x]})"; }
            }
            // -1 : character not in word
            if (output[x] == -1)
            {
                // If Letter Already Checked But Is Not At Current Index, Only Mark Index
                if (checkedLetters.Contains(input[x])) { 
                    if(!indexRegex[x].Contains(input[x])) { indexRegex[x] = indexRegex[x].Insert(3, input[x].ToString()); } 
                }
                // Else, If letter Not Already Checked, Mark All Indexes
                else
                {
                    for (int y = 0; y < 5; y++)
                    {
                        // Skipping Indexes With Found Letter (Regex Length = 1 E.g. "a" not "[^1a...]")
                        if (indexRegex[y].Length > 1) { 
                            if(!indexRegex[y].Contains(input[x])) { indexRegex[y] = indexRegex[y].Insert(3, input[x].ToString()); } }
                    }
                }
            }
            checkedLetters.Add(input[x]);
        }
    }
}