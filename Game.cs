using System;
using System.Collections.Generic;

namespace StarFix
{
    internal class Game
    {
        private Player player = null!;
        private List<Question> questions = new List<Question>();
        private Random random = new Random();

        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== STARFIX SPACE QUIZ ===");
            Console.Write("Enter Pilot Name: ");

            string name = Console.ReadLine() ?? "Pilot";
            player = new Player(name, 3);

            questions = new List<Question>
            {
                new Question
                {
                    QuestionText = "Which planet is known as the Red Planet?",
                    Options = new List<string> { "Earth", "Mars", "Jupiter" },
                    CorrectOptionIndex = 1
                },
                new Question
                {
                    QuestionText = "What is the name of our galaxy?",
                    Options = new List<string> { "Andromeda", "Milky Way", "Orion" },
                    CorrectOptionIndex = 1
                },
                new Question
                {
                    QuestionText = "What do stars mainly produce?",
                    Options = new List<string> { "Water", "Energy", "Ice" },
                    CorrectOptionIndex = 1
                },
                new Question
                {
                    QuestionText = "Which planet is the largest in our solar system?",
                    Options = new List<string> { "Saturn", "Jupiter", "Neptune" },
                    CorrectOptionIndex = 1
                }
            };

            // Shuffle questions
            for (int i = 0; i < questions.Count; i++)
            {
                int j = random.Next(i, questions.Count);
                (questions[i], questions[j]) = (questions[j], questions[i]);
            }
        }

        public void Run()
        {
            StartGame();

            foreach (var q in questions)
            {
                if (!player.IsAlive())
                    break;

                q.Display();

                Console.Write($"\n{player.Name}, enter choice (1-{q.Options.Count}): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (q.CheckAnswer(choice))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Correct! System stabilized.");
                        Console.ResetColor();

                        player.AddScore(10);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong! System failure detected.");
                        Console.ResetColor();

                        player.LoseLife();
                        Console.WriteLine($"Lives left: {player.Lives}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }

            EndGame();
        }

        public void EndGame()
        {
            Console.WriteLine("\n=== MISSION REPORT ===");

            if (player.IsAlive())
                Console.WriteLine("Mission successful. All systems stabilized.");
            else
                Console.WriteLine("Mission failed. Critical system loss.");

            Console.WriteLine($"Final Score: {player.Score}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}