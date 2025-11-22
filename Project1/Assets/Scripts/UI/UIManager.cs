using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private MainMenu mainMenu;

    public void ShowMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
        
    }
    public void UpdateRoomDescription(string description)
    {
        inGameUI.PrintRoomDescription(description);
    }
}
