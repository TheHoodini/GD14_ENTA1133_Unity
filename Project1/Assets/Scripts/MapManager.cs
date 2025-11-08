using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private RoomBase[] RoomPrefabs;
    [SerializeField] private float RoomSize = 1;
    [SerializeField] private int MapSize = 1;

    public void CreateMap()
    {
        for (int x = 0; x < MapSize; x++)
        {
            for (int z = 0; z < MapSize; z++)
            {
                Vector3 coords = new Vector3(x * RoomSize, 0, z * RoomSize);

                var roomInstance = Instantiate(
                    RoomPrefabs[Random.Range(0, RoomPrefabs.Length)],
                    transform
                );

                roomInstance.transform.position = coords;
                //roomInstance.transform.position = new Vector3(coords.x, 0, coords.z);
            }
        }

        // Set doorways
        for (int x = 0; x < MapSize; x++)
        {
            for (int z = 0; z < MapSize; z++)
            {
                int roomIndex = x * MapSize + z;

                RoomBase currentRoom = transform.GetChild(roomIndex).GetComponent<RoomBase>();
                RoomBase northRoom = (z < MapSize - 1) ? transform.GetChild(x * MapSize + (z + 1)).GetComponent<RoomBase>() : null;
                RoomBase eastRoom = (x < MapSize - 1) ? transform.GetChild((x + 1) * MapSize + z).GetComponent<RoomBase>() : null;
                RoomBase southRoom = (z > 0) ? transform.GetChild(x * MapSize + (z - 1)).GetComponent<RoomBase>() : null;
                RoomBase westRoom = (x > 0) ? transform.GetChild((x - 1) * MapSize + z).GetComponent<RoomBase>() : null;

                currentRoom.SetDoorways(northRoom, eastRoom, southRoom, westRoom);
            }
        }
    }

}
