using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RetrieveHighscores : MonoBehaviour
{
    private ReadOnlyCollection<KeyValuePair<int, string>>[] Scores;

    public GameObject DifficultyText, NoScoresText, NameDisplay, ScoreDisplay;

    // In Unity it is recommended to use Awake or Start instead of a constructor for MonoBehaviours because of the way Unity's serialization works
    void Awake()
    {
        Scores = new ReadOnlyCollection<KeyValuePair<int, string>>[3];
    }

    void OnEnable()
    {
        SetDifficultyTitle("Easy");
        PopulateScoreDisplay(0);
    }

    public void SetDifficultyTitle(string difficultyName)
    {
        DifficultyText.GetComponent<Text>().text = "Highscores - " + difficultyName;
    }

    public void PopulateScoreDisplay(int difficulty)
    {
        if (Scores[difficulty] == null) Scores[difficulty] = Highscores.GetScoresAsReadOnly(difficulty);

        StringBuilder NameText = new StringBuilder(), ScoreText = new StringBuilder();
        int position = 1;
        foreach (KeyValuePair<int, string> Score in Scores[difficulty])
        {
            if (Score.Key > 0)
            {
                // The goal is to avoid creating multiple instances of strings while building the list, which means avoiding concatenations
                NameText.Append(position).Append(". ").AppendLine(Score.Value);
                ScoreText.Append(Score.Key).AppendLine();
                position++;
            }
            else break; // Since the highscore lists are sorted, once we find a score of 0, we know the rest of the scores are 0 too
        }
        
        NameDisplay.GetComponent<Text>().text = NameText.ToString();
        ScoreDisplay.GetComponent<Text>().text = ScoreText.ToString();

        if (position == 1)
        {
            // If position was never incremented, we didn't find any scores
            NoScoresText.SetActive(true);
        }
        else NoScoresText.SetActive(false);
    }
}
