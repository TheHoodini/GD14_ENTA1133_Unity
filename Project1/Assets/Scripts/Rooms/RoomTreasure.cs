using UnityEngine;

public class RoomTreasure : RoomBase
{

    [SerializeField] private int treasureID = 1;
    public override void OnRoomSearch()
    {
        switch (treasureID)
        {
            case 1:
                Debug.Log($"You search... And you find yourself in a treasure room!");
                break;
            case 2:
                Debug.Log($"You search... And find a precious treasure!");
                break;
            default:
                Debug.Log($"You search... And find a pile of gold coins!");
                break;
        }
    }
}
