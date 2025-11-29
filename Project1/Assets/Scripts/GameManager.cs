using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapManager GameMapPrefab;
    [SerializeField] private PlayerController PlayerPrefab;
    [SerializeField] private UIManager UIManagerPrefab;

    private MapManager _gameMap;
    private PlayerController _playerController;
    private UIManager _uiManager;

    void Start()
    {
        MainMenuScreen();
        SetupMap();
        SpawnPlayer();
    }

    private void MainMenuScreen()
    { 
        _uiManager = Instantiate(UIManagerPrefab, transform);
        _uiManager.ShowMainMenu();
    }

    private void SetupMap()
    {
        Debug.Log("Game Started");
        transform.position = Vector3.zero;

        _gameMap = Instantiate(GameMapPrefab, transform);
        _gameMap.transform.position = Vector3.zero;

        _gameMap.CreateMap();

        Debug.Log("Map Created");
    }

    private void SpawnPlayer()
    {
        Debug.Log("Spawning Player");
        var startingRoom = _gameMap.MapRooms[
            Random.Range(0, _gameMap.MapRooms.GetLength(0)), 
            Random.Range(0, _gameMap.MapRooms.GetLength(1))
        ];
        _playerController = Instantiate(PlayerPrefab, transform);
        _playerController.transform.position = new Vector3(startingRoom.transform.position.x, 0, startingRoom.transform.position.z);
        _playerController.Setup(_uiManager);

        _playerController.SetStartingRoom(startingRoom, _gameMap);
        Debug.Log("Player Spawned");
    }
}
