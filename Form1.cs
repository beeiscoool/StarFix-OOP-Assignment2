using System;
using System.Windows.Forms;

namespace StarFixGUI
{
    public partial class Form1 : Form
    {
        private Game game;
        private int timeLeft = 30;

        public Form1()
        {
            InitializeComponent();
            gameTimer.Interval = 1000;
            gameTimer.Tick += gameTimer_Tick;

            btnOption1.Enabled = false;
            btnOption2.Enabled = false;
            btnOption3.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string playerName = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show("Please enter your name.");
                return;
            }

            game = new Game(playerName);
            timeLeft = 30;

            lblPlayer.Text = "Player: " + game.Player.Name;
            lblScore.Text = "Score: " + game.Player.Score;
            lblLives.Text = "Lives: " + game.Player.Lives;
            lblTimer.Text = "Time: " + timeLeft;
            lblResult.Text = "";

            btnOption1.Enabled = true;
            btnOption2.Enabled = true;
            btnOption3.Enabled = true;

            ShowQuestion();
            gameTimer.Start();
        }

        private void ShowQuestion()
        {
            if (game == null || !game.HasMoreQuestions())
            {
                EndGame();
                return;
            }

            Question q = game.GetCurrentQuestion();

            lblQuestion.Text = q.QuestionText;
            btnOption1.Text = q.Options[0];
            btnOption2.Text = q.Options[1];
            btnOption3.Text = q.Options[2];

            lblScore.Text = "Score: " + game.Player.Score;
            lblLives.Text = "Lives: " + game.Player.Lives;
        }

        private void CheckAnswer(int choice)
        {
            bool correct = game.SubmitAnswer(choice);

            if (correct)
                lblResult.Text = "Correct! System stabilized.";
            else
                lblResult.Text = "Wrong! System failure detected.";

            lblScore.Text = "Score: " + game.Player.Score;
            lblLives.Text = "Lives: " + game.Player.Lives;

            if (game.IsGameOver())
                EndGame();
            else
                ShowQuestion();
        }

        private void btnOption1_Click(object sender, EventArgs e)
        {
            CheckAnswer(1);
        }

        private void btnOption2_Click(object sender, EventArgs e)
        {
            CheckAnswer(2);
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            CheckAnswer(3);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            lblTimer.Text = "Time: " + timeLeft;

            if (timeLeft <= 0)
            {
                gameTimer.Stop();
                EndGame();
            }
        }

        private void EndGame()
        {
            gameTimer.Stop();

            btnOption1.Enabled = false;
            btnOption2.Enabled = false;
            btnOption3.Enabled = false;

            if (game != null)
                MessageBox.Show(game.GetFinalMessage(), "Mission Report");
        }

        private void lblLives_Click(object sender, EventArgs e)
        {
        }

        
        

        }
    }
