using System;
using System.Collections.Generic;

namespace StarFix
{
    internal class Question
    {
        public string QuestionText { get; set; } = "";
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectOptionIndex { get; set; }

        public Question()
        {
            Options = new List<string>();
        }

        public void Display()
        {
            Console.WriteLine("\nQ: " + QuestionText);

            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Options[i]}");
            }
        }

        public bool CheckAnswer(int choice)
        {
            return (choice - 1) == CorrectOptionIndex;
        }
    }
}