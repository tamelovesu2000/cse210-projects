// Exceeded Core Requirements:
// This program tracks the number of mindfulness activities
// completed during the session and displays the total
// when the user exits the program.

using System;
using System.Threading;
int completedActivities = 0;
bool running = true;

while (running)
{
    Console.Clear();
    Console.WriteLine("Menu Options:");
    Console.WriteLine("  1. Start breathing activity");
    Console.WriteLine("  2. Start reflection activity");
    Console.WriteLine("  3. Start listing activity");
    Console.WriteLine("  4. Quit");
    Console.Write("Select a choice from the menu: ");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        BreathingActivity activity = new BreathingActivity();
        activity.Run();
        completedActivities++;

    }
    else if (choice == "2")
    {
        ReflectionActivity activity = new ReflectionActivity();
        activity.Run();
        completedActivities++;
    }
    else if (choice == "3")
    {
        ListingActivity activity = new ListingActivity();
        activity.Run();

        completedActivities++;
    }
    else if (choice == "4")
    {
        Console.WriteLine();
        Console.WriteLine($"You completed {completedActivities} mindfulness activities this session.");
        Console.WriteLine("Thank you for using the Mindfulness Program!");
        Thread.Sleep(3000);

        running = false;
    }
    else
    {
        Console.WriteLine("Invalid choice.");
        Thread.Sleep(1500);
    }
}
