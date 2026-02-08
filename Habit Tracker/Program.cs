using System.Text.Json;
using System.IO;

Dictionary<string, int> habits;

if (File.Exists("Habits.json"))
{
    string json = File.ReadAllText("Habits.json");
    habits = JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    habits = new Dictionary<string, int>(habits, StringComparer.OrdinalIgnoreCase);
}
else
{
    habits = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
}
    while (true)
    {
        Console.WriteLine("[1] Add habit.");
        Console.WriteLine("[2] Show all habits and streaks.");
        Console.WriteLine("[3] Show streak.");
        Console.WriteLine("[4] Exit.");

        var pressedKey = Console.ReadKey(true);

        if (pressedKey.Key == ConsoleKey.D1 || pressedKey.Key == ConsoleKey.NumPad1)
        {
            while (true)
            {
                Console.Write("Please enter a habit: ");
                string temp;
                bool isString = !string.IsNullOrWhiteSpace(temp = Console.ReadLine());

                if (isString)
                {
                    if (habits.ContainsKey(temp))
                    {
                        habits[temp]++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"+1 streak for {temp}!");
                        Console.ResetColor();
                    }
                    else
                    {
                        habits.Add(temp, 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{temp} added successfully!");
                        Console.ResetColor();
                    }
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid habit.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }
            }
        }

        if (pressedKey.Key == ConsoleKey.D2 || pressedKey.Key == ConsoleKey.NumPad2)
        {
            foreach (var habit in habits)
            {
                Console.WriteLine($"{habit.Key}: {habit.Value}");
            }
            Console.ReadKey();
            continue;
        }

        if (pressedKey.Key == ConsoleKey.D3 || pressedKey.Key == ConsoleKey.NumPad3)
        {
            while (true)
            {
                Console.WriteLine("Please enter the name of the habit: ");
                string temp;
                bool isString = !string.IsNullOrWhiteSpace(temp = Console.ReadLine());

                if (isString)
                {
                    if (habits.ContainsKey(temp))
                    {
                        Console.WriteLine($"{temp}: {habits[temp]}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"There is no habit named \"{temp}\"");
                        Console.ResetColor();
                    }
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid name.");
                    Console.ResetColor();
                    continue;
                }
            }
        }

        if (pressedKey.Key == ConsoleKey.D4 || pressedKey.Key == ConsoleKey.NumPad4)
        {
            string json = JsonSerializer.Serialize(habits);
            File.WriteAllText("Habits.json", json);
            break;
        }
    }