using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapManager GameMapPrefab;
    private MapManager _gameMap;

    void Start()
    {
        Debug.Log("Game Started");
        transform.position = Vector3.zero;

        _gameMap = Instantiate(GameMapPrefab, transform);
        _gameMap.transform.position = Vector3.zero;

        _gameMap.CreateMap();

        Debug.Log("Map Created");
    }

    private void StartGame()
    {
        Debug.Log("Game Started");
    }
}
