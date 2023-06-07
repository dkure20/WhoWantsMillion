using System;
using System.Collections;
using System.Text.Json;
using System.IO;
using System.Text;

namespace WhoWantsMillione;
public class Program
{
    public static void Main()
    {   
        Program program = new Program();
        GenerateQuestions validation = new GenerateQuestions();
        validation.Generate();
        string fileName = "C:\\Users\\dkureli\\source\\repos\\WhoWantsMillione\\WhoWantsMillione\\questions.txt";
        string persons = File.ReadAllText(fileName);
        program.StartGame(persons);  
    }

    private void StartGame(string persons)
    {
        List<Person> personList;
        if (string.IsNullOrEmpty(persons)) personList = new List<Person>();
        else personList = JsonSerializer.Deserialize<List<Person>>(persons);
        Person gamePerson = FindPerson(personList);
        int winningStreak = 0;
        int winningMoney = 0;
        List<Question> questionList = gamePerson.userQuestions;
        foreach (var item in questionList)
        {
            Console.WriteLine("Question: " + item.questionName);
            item.ShuffleAndPrintAnswers(item.answers);
            Console.WriteLine("Please Choose the correct answer:");
            string correctAns = Console.ReadLine();
            if(correctAns == item.correctAnswer)
            {
                GuessInformation(ref winningStreak, gamePerson,ref winningMoney);
                if (winningStreak == 2)
                {
                    Console.WriteLine("You won Million dollar");
                    break;
                }
                Console.WriteLine("Do you want to continue?");
                string continueGame = Console.ReadLine();
                if (continueGame == "no")
                {
                    Console.WriteLine("Congratulations: you won " + winningMoney + " dollars!");
                    break;
                }
            }
            else
            {
                IncorrectGuessInformation(ref winningStreak, gamePerson, ref winningMoney);
                break;
            }
        }
        List<Person> updatedLeaderBoard = UpdateLeaderBoard(gamePerson);
        ShowLeaderBoard(updatedLeaderBoard);
    }

    private List<Person> UpdateLeaderBoard(Person gamePerson)
    {
        string leaderboardFile = @"C:\Users\dkureli\source\repos\WhoWantsMillione\WhoWantsMillione\leaderboard.txt";
        string leaderBoard = File.ReadAllText(leaderboardFile);
        List<Person> personLeaderBoards;
        if (string.IsNullOrEmpty(leaderBoard)) personLeaderBoards = new List<Person>();
        else personLeaderBoards = JsonSerializer.Deserialize<List<Person>>(File.ReadAllText(leaderboardFile));
        personLeaderBoards.Add(gamePerson);
        string data = JsonSerializer.Serialize(personLeaderBoards, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(leaderboardFile, data);
        return personLeaderBoards;
    }

    private void ShowLeaderBoard(List<Person> personList)
    {
       List<Person> persons = personList.OrderByDescending(person=>person.moneyAmount).ToList();
        for(int i=0; i<persons.Count; i++)
        {
            Person person = persons[i];
            Console.WriteLine("Place " + (i + 1) + ": " + person.personId + ", Money: " + person.moneyAmount);
        }
    }

    private void IncorrectGuessInformation(ref int winningStreak, Person gamePerson, ref int winningMoney)
    {
        winningStreak = 0;
        winningMoney = 0;
        gamePerson.moneyAmount = 0;
        Console.WriteLine("Sorry, you lose everything");
    }

    private void GuessInformation(ref int winningStreak, Person gamePerson, ref int winningMoney)
    {
        winningStreak++;
        winningMoney = CalculateMoney(winningStreak);
        Console.WriteLine("You guess it, and won:" + winningMoney);
        gamePerson.moneyAmount = winningMoney;
    }

    private Person FindPerson(List<Person> personList)
    {
       return personList[personList.Count - 1];
    }

    private static int CalculateMoney(int winningStreak)
    {
        switch(winningStreak)
        {
            case 1: return 50;
            case 2: return 100;
            case 3: return 500;
            case 4: return 1000;
            case 5: return 5000;
            case 6: return 20000;
            case 7: return 100000;
            case 8: return 200000;
            case 9: return 500000;
            case 10:return 1000000;
        }
        return 0;
    }
}