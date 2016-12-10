using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static class Highscores
{
    private static List<KeyValuePair<int, string>>[] Scores; // These lists must stay sorted by score in descending order

    static Highscores()
    {
        Scores = new List<KeyValuePair<int, string>>[] { new List<KeyValuePair<int, string>>(10), new List<KeyValuePair<int, string>>(10), new List<KeyValuePair<int, string>>(10) };
        for (int difficulty = 0; difficulty < 3; difficulty++)
        {
            for (int i = 0; i < 10; i++)
            {
                /* 10 highscores are stored for each difficulty, they are stored under the key [0(Easy)-2(Hard)]Score[0-9] with a corresponding [0(Easy)-2(Hard)]Name[0-9]
                 * They are saved in a sorted state, so the lists don't need to be sorted after loading */
                Scores[difficulty].Add(new KeyValuePair<int, string>(PlayerPrefs.GetInt(difficulty + "Score" + i), PlayerPrefs.GetString(difficulty + "Name" + i)));
            }
        }
    }

    // Since the lowest highscore is stored at the 9th position, this method checks whether a new score is larger, making the new score a highscore
    public static bool ConsiderScore(int score, int difficulty)
    {
        if (score > Scores[difficulty][9].Key) return true;
        return false;
    }

    public static void AddScore(int score, int difficulty, string name)
    {
        Scores[difficulty][9] = new KeyValuePair<int, string>(score, name);
        //TODO: Make highscore specific insertion instead of sorting entire array
        Scores[difficulty].Sort((x, y) => y.Key.CompareTo(x.Key));
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt(difficulty + "Score" + i, Scores[difficulty][i].Key);
            PlayerPrefs.SetString(difficulty + "Name" + i, Scores[difficulty][i].Value);
        }
        PlayerPrefs.Save();
    }

    public static ReadOnlyCollection<KeyValuePair<int, string>> GetScoresAsReadOnly(int difficulty)
    {
        return Scores[difficulty].AsReadOnly();
    }
}
