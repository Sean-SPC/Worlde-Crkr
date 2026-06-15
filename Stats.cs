using System.IO.Compression;
using System.Net;

class Stats
{
    private static List<string> wordList = File.ReadAllLines("five-ltr-words.txt").ToList();
    private static Dictionary<char, int> charStats = Enumerable.Range('a', 26)
                                  .Select(i => (char)i)
                                  .ToDictionary(c => c, i => 0);

    public static void GetStats()
    {
        foreach (string str in wordList)
        {
            foreach (char c in str) { charStats[c]++; }
        }
    }

    public static void PrintStatsToFile()
    {
        File.WriteAllText("char-stats", "");
        GetStats();

        File.AppendAllText("char-stats", "|| ALPHABETICAL ||\n\n");
        foreach (var item in charStats)
        {
            File.AppendAllText("char-stats", item.Key + " : " + item.Value + "\n");
        }

        File.AppendAllText("char-stats", "\n|| DESCENDING ||\n\n");
        foreach (var item in charStats.OrderByDescending(x => x.Value))
        {
            File.AppendAllText("char-stats", item.Key + " : " + item.Value + "\n");
        }
    }
}