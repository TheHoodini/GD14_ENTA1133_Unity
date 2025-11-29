using UnityEngine;

public class RoomCombat : RoomBase
{
    [SerializeField] private int combatID = 1;
    public override string OnRoomSearch()
    {
        string message;
        switch (combatID)
        {
            case 1:
                message = "You are in a combat room, be careful, an enemy must be sneaking!";
                break;
            case 2:
                message = "You search... And see an evil robot!";
                break;
            default:
                message = "You search... And see a giant robot!";
                break;
        }
        Debug.Log(message);
        return message;
    }
}
