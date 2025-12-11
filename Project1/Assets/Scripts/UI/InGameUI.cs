using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomDesc;
    [SerializeField] private TextMeshProUGUI coinsAmount;

    public void ButtonStartGame()
    {
        gameObject.SetActive(true);
    }
    public void PrintRoomDescription(string description)
    {
        roomDesc.text = ">" + description;
    }

    public void PrintCoinsAmount(string amount) 
    { 
        coinsAmount.text = amount; 
    }
}
