using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private RoomBase[] roomPrefabs;
    [SerializeField] private float roomSize = 8;
    [SerializeField] private int mapSize = 5;

    public int MapSize => mapSize;
    public float RoomSize => roomSize;

    RoomBase[,] mapRooms;
    public RoomBase[,] MapRooms
    {
        get
        {
            if (mapRooms == null)
            {
                mapRooms = new RoomBase[mapSize, mapSize];
            }
            return mapRooms;
        }
    }

    public void CreateMap()
    {
        mapRooms = new RoomBase[mapSize, mapSize];
        for (int x = 0; x < mapSize; x++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                Vector3 coords = new Vector3(x * roomSize, 0, z * roomSize);

                var roomInstance = Instantiate(
                    roomPrefabs[Random.Range(0, roomPrefabs.Length)],
                    transform
                );

                roomInstance.transform.position = coords;
                roomInstance.X = x;
                roomInstance.Z = z;
                mapRooms[x, z] = roomInstance;
                //roomInstance.transform.position = new Vector3(coords.x, 0, coords.z);
            }
        }

        // Set doorways
        for (int x = 0; x < mapSize; x++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                int roomIndex = x * mapSize + z;

                RoomBase currentRoom = transform.GetChild(roomIndex).GetComponent<RoomBase>();
                RoomBase northRoom = (z < mapSize - 1) ? transform.GetChild(x * mapSize + (z + 1)).GetComponent<RoomBase>() : null;
                RoomBase eastRoom = (x < mapSize - 1) ? transform.GetChild((x + 1) * mapSize + z).GetComponent<RoomBase>() : null;
                RoomBase southRoom = (z > 0) ? transform.GetChild(x * mapSize + (z - 1)).GetComponent<RoomBase>() : null;
                RoomBase westRoom = (x > 0) ? transform.GetChild((x - 1) * mapSize + z).GetComponent<RoomBase>() : null;

                currentRoom.SetDoorways(northRoom, eastRoom, southRoom, westRoom);
            }
        }
    }

}
