using UnityEngine;

public class RoomCombat : RoomBase
{
    public override void OnRoomSearch()
    {
        Debug.Log($"You search... And find an enemy! Be careful!");
    }
}
