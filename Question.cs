using System.Collections.Generic;

namespace StarFixGUI
{
    internal class Question
    {
        private string questionText;
        private List<string> options;
        private int correctOptionIndex;

        public string QuestionText
        {
            get { return questionText; }
            set { questionText = value; }
        }

        public List<string> Options
        {
            get { return options; }
            set { options = value; }
        }

        public int CorrectOptionIndex
        {
            get { return correctOptionIndex; }
            set { correctOptionIndex = value; }
        }

        public Question()
        {
            options = new List<string>();
        }

        public Question(string questionText, List<string> options, int correctOptionIndex)
        {
            this.questionText = questionText;
            this.options = options;
            this.correctOptionIndex = correctOptionIndex;
        }

        public bool CheckAnswer(int choice)
        {
            return (choice - 1) == correctOptionIndex;
        }
    }
}