using GD14_1133_A1_JuanDiego_DiceGame;
using GD14_1133_A1_JuanDiego_DiceGame.Scripts;
using JetBrains.Annotations;
using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class DiceGame : MonoBehaviour
{
    public string playerDice = "d20";
    public string CPUDice = "d20";

    public int mapSize = 5;
    void Start()
    {
        PlayerClass player = new PlayerClass("Hoodini");
        PlayerClass cpuPlayer = new PlayerClass("CPU");

        DieRoller diceRoller = new DieRoller();

        DateTime dateToday = DateTime.Now;
        dateToday.ToString("dd/MM/yyyy");
        Debug.Log($"{dateToday}\nWelcome Player to the Dice Game!\n");

        player.Score += diceRoller.Roll(playerDice);
        cpuPlayer.Score += diceRoller.Roll(CPUDice, false);

        if (player.Score > cpuPlayer.Score)
        {
            Debug.Log($"Congratulations {player.Name}, you won with a score of {player.Score}!");
        }
        else if (player.Score < cpuPlayer.Score)
        {
            Debug.Log($"Sorry {player.Name}, you lost against a score of {cpuPlayer.Score}.");        }
        else
        {
            Debug.Log($"It's a tie!");
        }

        VisualizeMap();
        Debug.Log("Map printed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void VisualizeMap()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                var mapRoomRepresentation = GameObject.CreatePrimitive(PrimitiveType.Cube);
                mapRoomRepresentation.transform.position = new Vector3(x, 10, z);
            }
        }
    }

}
