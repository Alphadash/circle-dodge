using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void SetSize(int size)
    {
        GenerateGrid.GridSize = size;
    }
}
