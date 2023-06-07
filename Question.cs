using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWantsMillione
{
    public class Question : IQuestion
    {
        public string questionName { get; set; }
        public List<string> answers { get; set; }
        public string correctAnswer { get; set; }

        public Question()
        {
            questionName = "";
            answers = new List<string>();
            correctAnswer = "";
        }
        public bool IsValidAnswer(string correctAnswer)
        {
            return answers.Contains(correctAnswer);
        }

        public void ShuffleAndPrintAnswers(List<string> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            foreach (string answer in list)
            {
                Console.WriteLine("Answer: " + answer);
            }
        }
    }
}
