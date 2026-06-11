using System.Formats.Asn1;
using System.Text.RegularExpressions;

class Test
{
    public static void run(string testAnswer)
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
        List<char> lettersInWord = new List<char>();

        while (true)
        {
            // Thread.Sleep(15000);

            // Input Word
            // Use Top of Wordlist
            string input = wordList[0];
            Console.WriteLine("Input: " + input);
            List<int> output = [.. new int[5]];

            for (int x = 0; x < 5; x++)
            {
                if (input[x] == testAnswer[x]) { output[x] = 1; }
                else
                {
                    if (testAnswer.Contains(input[x])) { output[x] = 0; }
                    else { output[x] = -1; }
                }
            }

            Console.WriteLine(string.Join(", ", output));

            if (output.All(x => x == 1)) { Console.WriteLine("Word Found! : " + input); break; }

            // Handle Output
            for (int x = 0; x < 5; x++)
            {
                // 1 : character in word, in right place
                if (output[x] == 1) { 
                    if (indexPatterns[x] == input[x].ToString()) { continue; }
                    indexPatterns[x] = input[x].ToString(); lettersInWord.Add(input[x]); 
                }
                // -1 : character not in word
                if (output[x] == -1)
                {
                    // checking for double letters
                    if (lettersInWord.Contains(input[x])) { indexPatterns[x] = indexPatterns[x].Insert(3, input[x].ToString()); continue; }

                    for (int y = 0; y < 5; y++)
                    {
                        if (indexPatterns[y].Length > 1)
                        {
                            indexPatterns[y] = indexPatterns[y].Insert(3, input[x].ToString());
                        }
                    }
                }
                // 0 : character in word, in wrong place
                if (output[x] == 0) { indexPatterns[x] = indexPatterns[x].Insert(3, input[x].ToString()); containRegex += $"(?=.*{input[x]})"; }
            }
            ;

            // Construct Pattern
            regexPattern = "^";
            regexPattern += containRegex;
            foreach (string str in indexPatterns) { regexPattern += str; }
            regexPattern += "$";

            Console.WriteLine(regexPattern);

            Console.WriteLine(Regex.IsMatch(input, regexPattern));

            // Filter Word List
            wordList.RemoveAll(word => !Regex.IsMatch(word, regexPattern));
        }
    }
}