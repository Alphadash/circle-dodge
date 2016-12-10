using UnityEngine;
using UnityEngine.UI;

public class RetrieveDifficultyText : MonoBehaviour
{
	void Start()
    {
        gameObject.GetComponent<Text>().text = "Difficulty: " + Gameplay.GetCurrentDifficultyName();
    }
}
