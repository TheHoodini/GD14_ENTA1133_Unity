using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomDesc;

    public void ButtonStartGame()
    {
        gameObject.SetActive(true);
    }
    public void PrintRoomDescription(string description)
    {
        roomDesc.text = ">" + description;
    }
}
