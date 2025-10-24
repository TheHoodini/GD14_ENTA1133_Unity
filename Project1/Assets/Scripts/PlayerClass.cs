using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_A1_JuanDiego_DiceGame.Scripts
{
    internal class PlayerClass
    {
        private string name;
        int score = 0;

        public PlayerClass(string playerName)
        {
            name = playerName;
        }
        public string Name => name;
        public int Score { 
            get => score; 
            set => score = value;
        }
    }
}
