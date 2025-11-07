using UnityEngine;

public class RoomBase : MonoBehaviour
{
    [SerializeField] private GameObject NorthDoorway, EastDoorway, SouthDoorway, WestDoorway;

    public void SetDoorways(RoomBase northRoom, RoomBase eastRoom, RoomBase southRoom, RoomBase westRoom)
    {
        NorthDoorway.SetActive(northRoom == null);
        EastDoorway.SetActive(eastRoom == null);
        SouthDoorway.SetActive(southRoom == null);
        WestDoorway.SetActive(westRoom == null);
    }
}
