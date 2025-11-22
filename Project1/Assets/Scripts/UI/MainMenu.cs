using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void ButtonStartGame()
    {
        gameObject.SetActive(false);
    }

    public void ButtonQuitGame()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
