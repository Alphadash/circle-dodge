using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public GameObject GameOverPanel, NavigationPanel, HighscorePanel, NewHighscoreText, ScoreDisplay;

    private int Score;
    private float TimeSinceScoreIncrease;

    public const float LaserZPosition = -4, BeamZPosition = 1, PlayerZPosition = -2, TileZPosition = -1; // Beams are children of Lasers making their position relative (-4 + 1 = -3)

    private static string[] Difficulties = { "Easy", "Medium", "Hard" };

	void FixedUpdate()
	{
		if (Player.IsAlive)
        {
            TimeSinceScoreIncrease += Time.deltaTime;

            if (TimeSinceScoreIncrease >= 1)
            {
                Score += 10;
                TimeSinceScoreIncrease -= 1;
                ScoreDisplay.GetComponent<Text>().text = "Score: " + Score;
            }
        }
	}

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        ScoreDisplay.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -5);

        if (Highscores.ConsiderScore(Score, GenerateGrid.GridSize - 3))
        {
            HighscorePanel.SetActive(true);
            NewHighscoreText.SetActive(true);
        }
        else NavigationPanel.SetActive(true);
    }

    public void SaveScore(string name)
    {
        Highscores.AddScore(Score, GenerateGrid.GridSize - 3, name);
        HighscorePanel.SetActive(false);
        NavigationPanel.SetActive(true);
    }

    public static string GetCurrentDifficultyName()
    {
        return Difficulties[GenerateGrid.GridSize - 3];
    }
}
