using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenManager : MonoBehaviour
{
    public GameObject FirstEnabled;

    public void OpenScreen(GameObject screen)
    {
        gameObject.SetActive(false);
        screen.SetActive(true);
    }

    void OnEnable()
    {
        if (FirstEnabled != null) EventSystem.current.SetSelectedGameObject(FirstEnabled);
    }
}
