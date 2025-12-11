using UnityEngine;

public class RoomTreasure : RoomBase
{

    [SerializeField] private int treasureID = 1;
    [SerializeField] private GameObject treasureMesh;
    private bool wasSearched;
    public override string OnRoomSearch()
    {
        string message = "";
        if (wasSearched)
        {
            message = "You already searched this room";
        }
        else
        {
            int coins;
            switch (treasureID)
            {
                case 1:
                    message = "You search among the boxes... And find 15 coins!";
                    coins = 15;
                    break;
                case 2:
                    message = "You search inside the treasure... And find 25 coins!";
                    coins = 25;
                    break;
                default:
                    message = "You you take all the coins... They make a total of 50 coins!";
                    coins = 50;
                    break;
            }
            GameManager.Instance.AddCoins(coins);
            Debug.Log(message);
            treasureMesh.SetActive(false);
            wasSearched = true;
        }
        return message;
    }

    public override string OnRoomEnter()
    {
        return "You enter inside an lonely golden room...";
    }
}
