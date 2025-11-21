using UnityEngine;

public class RoomBase : MonoBehaviour
{
    [SerializeField] private GameObject NorthDoorway, EastDoorway, SouthDoorway, WestDoorway;
    public int X { get; set; }
    public int Z { get; set; }

    public void SetDoorways(RoomBase northRoom, RoomBase eastRoom, RoomBase southRoom, RoomBase westRoom)
    {
        NorthDoorway.SetActive(northRoom == null);
        EastDoorway.SetActive(eastRoom == null);
        SouthDoorway.SetActive(southRoom == null);
        WestDoorway.SetActive(westRoom == null);
    }


    public virtual void OnRoomSearch()
    {
        Debug.Log($"You search but find nothing of interest...");
    }
}
