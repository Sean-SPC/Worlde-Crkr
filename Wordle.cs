using System.Formats.Asn1;
using System.Numerics;
using System.Text.RegularExpressions;

class Wordle
{
    public static void run()
    {
        // Reading Word List of Five Letter Words
        List<string> wordList = File.ReadAllLines("five-ltr-words.txt").ToList();

        // Index Patterns
        List<string> indexPatterns = [
            "[^1]",
            "[^1]",
            "[^1]",
            "[^1]",
            "[^1]"
        ];

        // Program

        // Variables
        string regexPattern = "";
        string containRegex = "";

        while (true)
        {
            // Input Word
            string input = "";
            int[] output = new int[5];
            Console.Write("Input: ");
            try
            {
                input = Console.ReadLine().ToLower();
                Console.Write("Output: ");

                output = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            }
            catch (NullReferenceException e)
            {
               Console.WriteLine(e.Message); 
            }

            if (output.All(x => x == 1)) { Console.WriteLine("Word Found! : " + input); break; }

            // Handle Output
            for (int x = 0; x < 5; x++)
            {
                // 1 : character in word, in right place
                if (output[x] == 1) { indexPatterns[x] = input[x].ToString(); }
                // -1 : character not in word
                if (output[x] == -1) { indexPatterns[x] = indexPatterns[x].Insert(3, input[x].ToString()); }
                // 0 : character in word, in wrong place
                if (output[x] == 0) { indexPatterns[x] = indexPatterns[x].Insert(3, input[x].ToString()); containRegex += $"(?=.*{input[x]})"; }
            }
            ;

            // Construct Pattern
            regexPattern = "^";
            regexPattern += containRegex;
            foreach (string str in indexPatterns) { regexPattern += str; }
            regexPattern += "$";

            // Console.WriteLine(regexPattern);

            // Filter Word List
            wordList.RemoveAll(word => !Regex.IsMatch(word, regexPattern));

            Console.WriteLine($"\nAttempt: {wordList[0]}");
        }
    }
}