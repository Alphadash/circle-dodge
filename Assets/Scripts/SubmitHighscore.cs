using UnityEngine;
using UnityEngine.UI;

public class SubmitHighscore : MonoBehaviour
{
    public Gameplay Gameplay;
    public Text NameInput;

	public void SubmitScore()
    {
        if (NameInput.text != "")
        {
            Gameplay.SaveScore(NameInput.text);
        }
    }
}
