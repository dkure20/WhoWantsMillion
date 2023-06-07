using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WhoWantsMillione
{
    public class GenerateQuestions
    {
        private static readonly int QUESTION_NUM = 2;
        private static readonly int ANSWER_NUM = 4;
        public void Generate() {
            string fileName = "C:\\Users\\dkureli\\source\\repos\\WhoWantsMillione\\WhoWantsMillione\\questions.txt";
            Console.WriteLine("Enter your nickname please: ");
            List<Person> persons;
            string personId = Console.ReadLine();
            Person person = new Person();
            person.personId = personId;
            string gamePersons = File.ReadAllText(fileName);
            List<Question> questionList = new List<Question>();
            if (string.IsNullOrEmpty(gamePersons)) persons = new List<Person>();
            else { persons = JsonSerializer.Deserialize<List<Person>>(gamePersons);}
            for (int i = 0; i < QUESTION_NUM; i++)
            {
                Question MyQuestion = new Question();
                Console.WriteLine("User, please input your question" + (i+1)+ ": ");
                string question = Console.ReadLine();
                MyQuestion.questionName = question;
                Console.WriteLine("Input 4 answers for your question: ");
                for (int j = 1; j <= ANSWER_NUM; j++)
                {
                    Console.WriteLine("Answer " + j + ": ");
                    string answer = Console.ReadLine();
                    MyQuestion.answers.Add(answer);
                }
                Console.WriteLine("User, please input correct answer: ");
                string correctAnswer = ChooseCorrectAnswer(MyQuestion);
                MyQuestion.correctAnswer = correctAnswer;
                questionList.Add(MyQuestion);
            }
            person.userQuestions = questionList;
            persons.Add(person);
            string data = JsonSerializer.Serialize(persons, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(fileName, data);
        }

        private string ChooseCorrectAnswer(Question MyQuestion)
        {
            while (true)
            {
                string correctAnswer = Console.ReadLine();
                if (MyQuestion.IsValidAnswer(correctAnswer))
                {
                    Console.WriteLine("Correct answer is updated");
                    return correctAnswer;
                }
                else
                {
                    Console.WriteLine("Please choose answer from your answer list");
                }
            }

        }
    }
}