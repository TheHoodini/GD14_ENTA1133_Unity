using UnityEngine;

public class RoomTreasure : RoomBase
{

    [SerializeField] private int treasureID = 1;
    public override string OnRoomSearch()
    {
        string message = "";
        switch (treasureID)
        {
            case 1:
                message = "You search... And you find yourself in a treasure room!";
                break;
            case 2:
                message = "You search... And find a precious treasure!";
                break;
            default:
                message = "You search... And find a pile of gold coins!";
                break;
        }
        Debug.Log(message);
        return message;
    }
}
