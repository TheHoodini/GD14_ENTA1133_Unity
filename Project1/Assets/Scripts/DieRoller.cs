using GD14_1133_A1_JuanDiego_DiceGame.Scripts;
using System;
using System.Diagnostics;
using System.IO;

namespace GD14_1133_A1_JuanDiego_DiceGame
{
    internal class DieRoller
    {
        private readonly Random random = new();

        public int Roll(string dieType, bool isPlayer = true)
        {
            string numericPart = !string.IsNullOrEmpty(dieType) && dieType.Length > 1
                ? dieType.Substring(1)
                : string.Empty;

            if (!int.TryParse(numericPart, out int maxRoll) || maxRoll < 1)
            {
                UnityEngine.Debug.LogError($"Invalid die type '{dieType}'.");
                return 0;
            }

            // Generate a random roll between 1 and the max roll
            int rollResult = random.Next(1, maxRoll + 1);

            string name = isPlayer ? "You" : "The CPU";
            UnityEngine.Debug.Log($"{name} rolled a {dieType}... The result was a {rollResult}!");

            // Comment based on roll
            string comment = rollResult switch
            {
                int r when r == maxRoll => "Excellent, a maximum roll!!!",
                1 => "A critical fail...",
                int r when r == (maxRoll / 2) => "Well, the average",
                int r when r > (maxRoll / 2) => "Above average!",
                _ => "Below average..."
            };

            UnityEngine.Debug.Log(comment);
            return rollResult;
        }
    }
}
