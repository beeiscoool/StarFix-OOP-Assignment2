using System;

namespace StarFix
{
    internal class Player
    {
        private string name;
        private int lives;
        private int score;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public Player(string name, int lives)
        {
            this.name = name;
            this.lives = lives;
            score = 0;
        }

        public void AddScore(int points)
        {
            score += points;
        }

        public void LoseLife()
        {
            if (lives > 0)
                lives--;
        }

        public bool IsAlive()
        {
            return lives > 0;
        }
    }
}
