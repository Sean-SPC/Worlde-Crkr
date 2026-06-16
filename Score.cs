using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

class Score
{
       
    public static Dictionary<string, int> Simulate(Dictionary<string, int> wordList)
    {
        // For each word 
        foreach (var word in wordList)
        {
            int score = 0;
            // Find How Many Hits It Has (score)
            foreach (var target in wordList) { if(word.Key.Any(c => target.Key.Contains(c))) score++; }
            wordList[word.Key] = score;
        }

        // Order By Score Descending
        wordList = wordList.OrderByDescending(x => x.Value).ToDictionary();

        // // Write To File
        // File.WriteAllText("score.txt", "");
        // foreach (var word in wordList)
        // {
        //     File.AppendAllText("five-ltr-scores.txt", word.Key + " : " + word.Value + "\n");
        // }

        return wordList;
    }
}