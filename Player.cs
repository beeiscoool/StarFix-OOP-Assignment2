using System;

namespace StarFix
{
    internal class Player
    {
        public string Name { get; set; }
        public int Lives { get; set; }
        public int Score { get; set; }

        public Player(string name, int lives)
        {
            Name = name;
            Lives = lives;
            Score = 0;
        }

        public void AddScore(int points)
        {
            Score += points;
        }

        public void LoseLife()
        {
            Lives--;
        }

        public bool IsAlive()
        {
            return Lives > 0;
        }
    }
}