using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;

    static void Main()
    {
        int choice = 0;

        while (choice != 6)
        {
            Console.WriteLine();
            Console.WriteLine($"You have {score} points.");
            Console.WriteLine($"Current Level: {GetLevel()}");
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out choice))
            {
                if (choice == 1)
                {
                    CreateGoal();
                }
                else if (choice == 2)
                {
                    ListGoals();
                }
                else if (choice == 3)
                {
                    SaveGoals();
                }
                else if (choice == 4)
                {
                    LoadGoals();
                }
                else if (choice == 5)
                {
                    RecordEvent();
                }
                else if (choice == 6)
                {
                    Console.WriteLine("Thank you for using Eternal Quest!");
                }
                else
                {
                    Console.WriteLine("Please select a number from 1 to 6.");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine();
        Console.WriteLine("The types of goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

        string input = Console.ReadLine();

        if (!int.TryParse(input, out int goalType))
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? ");

        if (!int.TryParse(Console.ReadLine(), out int points))
        {
            Console.WriteLine("Invalid points.");
            return;
        }

        if (goalType == 1)
        {
            goals.Add(new SimpleGoal(name, description, points));
        }
        else if (goalType == 2)
        {
            goals.Add(new EternalGoal(name, description, points));
        }
        else if (goalType == 3)
        {
            Console.Write("How many times does this goal need to be completed? ");

            if (!int.TryParse(Console.ReadLine(), out int target))
            {
                Console.WriteLine("Invalid target.");
                return;
            }

            Console.Write("What is the bonus for completing the goal? ");

            if (!int.TryParse(Console.ReadLine(), out int bonus))
            {
                Console.WriteLine("Invalid bonus.");
                return;
            }

            goals.Add(new ChecklistGoal(
                name,
                description,
                points,
                target,
                bonus));
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        Console.WriteLine("Goal created successfully!");
    }

    static void ListGoals()
    {
        Console.WriteLine();

        if (goals.Count == 0)
        {
            Console.WriteLine("You do not have any goals yet.");
            return;
        }

        Console.WriteLine("Your Goals:");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetDetailsString()}");
        }
    }

    static void RecordEvent()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("You do not have any goals to record.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Which goal did you accomplish?");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetName()}");
        }

        Console.Write("Select a goal: ");

        if (!int.TryParse(Console.ReadLine(), out int goalNumber))
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        if (goalNumber < 1 || goalNumber > goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }

        Goal selectedGoal = goals[goalNumber - 1];

        bool wasComplete = selectedGoal.IsComplete();

        int pointsEarned = selectedGoal.RecordEvent();

        if (pointsEarned > 0)
        {
            score += pointsEarned;

            Console.WriteLine(
                $"Congratulations! You earned {pointsEarned} points!");

            ShowAchievement();
        }
        else if (wasComplete)
        {
            Console.WriteLine("This goal has already been completed.");
        }
    }

    static void SaveGoals()
    {
        Console.Write("Enter a filename to save: ");
        string filename = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Invalid filename.");
            return;
        }

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(score);

            foreach (Goal goal in goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved successfully!");
    }

    static void LoadGoals()
    {
        Console.Write("Enter a filename to load: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        if (lines.Length == 0)
        {
            Console.WriteLine("The file is empty.");
            return;
        }

        if (!int.TryParse(lines[0], out score))
        {
            Console.WriteLine("Invalid save file.");
            return;
        }

        goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }

            string[] parts = lines[i].Split('|');

            if (parts[0] == "SimpleGoal")
            {
                SimpleGoal goal = new SimpleGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]));

                bool completed = bool.Parse(parts[4]);

                if (completed)
                {
                    goal.RecordEvent();
                }

                goals.Add(goal);
            }
            else if (parts[0] == "EternalGoal")
            {
                EternalGoal goal = new EternalGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]));

                goals.Add(goal);
            }
            else if (parts[0] == "ChecklistGoal")
            {
                ChecklistGoal goal = new ChecklistGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]),
                    int.Parse(parts[5]),
                    int.Parse(parts[6]));

                int completed = int.Parse(parts[4]);

                for (int j = 0; j < completed; j++)
                {
                    goal.RecordEvent();
                }

                goals.Add(goal);
            }
        }

        Console.WriteLine("Goals loaded successfully!");
    }

    // Creativity feature:
    // The program now gives the user a level based on their total score.
    // This goes beyond the core requirements by adding a simple
    // gamification system to encourage the user to continue achieving goals.
    static string GetLevel()
    {
        if (score >= 5000)
        {
            return "Level 6 - Eternal Master";
        }
        else if (score >= 3000)
        {
            return "Level 5 - Quest Champion";
        }
        else if (score >= 2000)
        {
            return "Level 4 - Quest Expert";
        }
        else if (score >= 1000)
        {
            return "Level 3 - Quest Warrior";
        }
        else if (score >= 500)
        {
            return "Level 2 - Quest Beginner";
        }
        else
        {
            return "Level 1 - Starting the Quest";
        }
    }

    // Creativity feature:
    // The program gives achievement messages when the user earns points.
    static void ShowAchievement()
    {
        if (score >= 5000)
        {
            Console.WriteLine("Achievement: Eternal Master!");
        }
        else if (score >= 3000)
        {
            Console.WriteLine("Achievement: Quest Champion!");
        }
        else if (score >= 2000)
        {
            Console.WriteLine("Achievement: Quest Expert!");
        }
        else if (score >= 1000)
        {
            Console.WriteLine("Achievement: Quest Warrior!");
        }
        else if (score >= 500)
        {
            Console.WriteLine("Achievement: Quest Beginner!");
        }
    }
}