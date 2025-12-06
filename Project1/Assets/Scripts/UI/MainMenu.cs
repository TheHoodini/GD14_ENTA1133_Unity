using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void ButtonStartGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ButtonQuitGame()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
