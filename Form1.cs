using System;
using System.Windows.Forms;

namespace StarFixGUI
{
    public partial class Form1 : Form
    {
        private Game game;
        private int timeLeft;

        public Form1()
        {
            InitializeComponent();

            btnStart.Click += btnStart_Click;
            btnSubmit.Click += btnSubmit_Click;
            txtAnswer.KeyDown += txtAnswer_KeyDown;
            gameTimer.Tick += gameTimer_Tick;

            SetupForm();
        }

        private void SetupForm()
        {
            lblPlayer.Text = "Player: -";
            lblLevel.Text = "Level: -";
            lblQuestion.Text = "Enter your name and press Start to begin.";
            lblChoices.Text = "";
            lblScore.Text = "Score: 0";
            lblLives.Text = "Lives: 3";
            lblTimer.Text = "Time: 30";

            txtAnswer.Enabled = false;
            btnSubmit.Enabled = false;

            gameTimer.Interval = 1000;
            gameTimer.Stop();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter your name first.");
                return;
            }

            game = new Game(txtName.Text.Trim());

            lblPlayer.Text = "Player: " + game.PlayerName;

            txtAnswer.Enabled = true;
            btnSubmit.Enabled = true;

            DisplayQuestion();
            StartTimer();
        }

        private void DisplayQuestion()
        {
            if (game == null || game.IsGameOver())
            {
                return;
            }

            Question q = game.CurrentQuestion;

            lblPlayer.Text = "Player: " + game.PlayerName;
            lblLevel.Text = "Level: " + game.CurrentLevel.LevelName;
            lblQuestion.Text = q.QuestionText;

            lblChoices.Text =
                "1. " + q.Options[0] + Environment.NewLine +
                "2. " + q.Options[1] + Environment.NewLine +
                "3. " + q.Options[2];

            lblScore.Text = "Score: " + game.Score;
            lblLives.Text = "Lives: " + game.Lives;

            txtAnswer.Clear();
            txtAnswer.Focus();
        }

        private void StartTimer()
        {
            timeLeft = 30;
            lblTimer.Text = "Time: " + timeLeft;
            gameTimer.Start();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (game == null)
            {
                MessageBox.Show("Please start the game first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAnswer.Text))
            {
                MessageBox.Show("Please enter your answer first.");
                txtAnswer.Focus();
                return;
            }

            int userAnswer;

            if (!int.TryParse(txtAnswer.Text.Trim(), out userAnswer))
            {
                MessageBox.Show("Please enter only 1, 2, or 3.");
                txtAnswer.Clear();
                txtAnswer.Focus();
                return;
            }

            if (userAnswer < 1 || userAnswer > 3)
            {
                MessageBox.Show("Answer must be 1, 2, or 3 only.");
                txtAnswer.Clear();
                txtAnswer.Focus();
                return;
            }

            gameTimer.Stop();

            bool correct = game.CheckAnswer(userAnswer);

            if (correct)
            {
                MessageBox.Show("Correct!");
            }
            else
            {
                MessageBox.Show("Wrong answer!");
            }

            lblScore.Text = "Score: " + game.Score;
            lblLives.Text = "Lives: " + game.Lives;

            game.NextQuestion();

            if (game.IsGameOver())
            {
                EndGame();
                return;
            }

            DisplayQuestion();
            StartTimer();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            lblTimer.Text = "Time: " + timeLeft;

            if (timeLeft <= 0)
            {
                gameTimer.Stop();

                MessageBox.Show("Time's up!");

                if (game != null)
                {
                    game.LoseLife();

                    lblScore.Text = "Score: " + game.Score;
                    lblLives.Text = "Lives: " + game.Lives;

                    game.NextQuestion();

                    if (game.IsGameOver())
                    {
                        EndGame();
                        return;
                    }

                    DisplayQuestion();
                    StartTimer();
                }
            }
        }

        private void EndGame()
        {
            gameTimer.Stop();

            txtAnswer.Enabled = false;
            btnSubmit.Enabled = false;

            lblPlayer.Text = "Player: " + game.PlayerName;
            lblScore.Text = "Score: " + game.Score;
            lblLives.Text = "Lives: " + game.Lives;

            if (game.IsWinner())
            {
                lblQuestion.Text = "Congratulations! You completed all levels!";
                lblChoices.Text = "";
                lblLevel.Text = "Level: Completed";
                lblTimer.Text = "Time: 0";
                MessageBox.Show("Well done! You completed Easy, Medium, and Hard!");
            }
            else
            {
                lblQuestion.Text = "Game Over!";
                lblChoices.Text = "";
                lblLevel.Text = "Level: Ended";
                lblTimer.Text = "Time: 0";
                MessageBox.Show("Game Over! You ran out of lives.");
            }
        }

        private void txtAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtAnswer.Text))
            {
                btnSubmit.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}