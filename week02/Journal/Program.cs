using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        int choice = 0;

        while (choice != 5)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Entry entry = new Entry();

                string prompt = promptGenerator.GetRandomPrompt();

                Console.WriteLine(prompt);
                Console.Write("> ");

                entry._promptText = prompt;
                entry._entryText = Console.ReadLine();
                entry._date = DateTime.Now.ToShortDateString();

                journal.AddEntry(entry);
            }

            if (choice == 2)
            {
                journal.DisplayAll();
            }

            if (choice == 4)
            {
                Console.Write("Enter the filename: ");
                string fileName = Console.ReadLine();

                journal.SaveToFile(fileName);

                Console.WriteLine("Journal saved successfully.");
            }
        }
    }
}