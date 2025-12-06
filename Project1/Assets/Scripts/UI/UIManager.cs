using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;

    public bool canPause;

    public void CanPause()
    {
        canPause = true;
    }

    public void ShowMainMenu()
    {
        canPause = false;
        Time.timeScale = 0f;
        mainMenu.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
    }
    public void UpdateRoomDescription(string description)
    {
        inGameUI.PrintRoomDescription(description);
    }

    public void ShowPauseMenu() 
    {
        inGameUI.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePauseMenu()
    {
        inGameUI.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
