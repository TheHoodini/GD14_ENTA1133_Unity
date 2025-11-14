using UnityEngine;

public class RoomTreasure : RoomBase
{
    public override void OnRoomSearch()
    {
        Debug.Log($"You search... And find a shiny treasure!");
    }
}
