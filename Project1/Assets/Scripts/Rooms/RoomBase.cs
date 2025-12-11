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


    public virtual string OnRoomSearch()
    {
        string message = "You search but find nothing of interest...";
        Debug.Log(message);
        return message;
    }

    public virtual string OnRoomEnter()
    {
        string message = "You enter inside an empty room...";
        Debug.Log(message);
        return message;
    }
}
