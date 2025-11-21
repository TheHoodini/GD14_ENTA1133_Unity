using UnityEngine;

public class RoomCombat : RoomBase
{
    [SerializeField] private int combatID = 1;
    public override void OnRoomSearch()
    {
        switch (combatID)
        {
            case 1:
                Debug.Log($"You are in a combat room, be careful, an enemy must be sneaking!");
                break;
            case 2:
                Debug.Log($"You search... And see an evil robot!");
                break;
            default:
                Debug.Log($"You search... And see a giant robot!");
                break;
        }
    }
}
