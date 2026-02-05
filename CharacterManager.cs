namespace W03;

/// <summary>
/// CharacterManager - THE PROBLEM (SRP Violation!)
///
/// LOOK AT THIS CLASS CAREFULLY. It does MANY different things:
/// 1. Reads from files (DisplayCharacters reads lines)
/// 2. Writes to files (AddCharacter would append)
/// 3. Searches data (FindCharacter)
/// 4. Handles user interface (menus, prompts)
/// 5. Business logic (LevelUpCharacter increments level)
///
/// This violates the Single Responsibility Principle!
/// Why is this bad?
/// - Hard to test (need file system for any test)
/// - Hard to reuse (can't use reading without the menu)
/// - Hard to change (modifying file format affects everything)
///
/// YOUR ASSIGNMENT: Refactor this into separate classes:
/// - Character (data class) - see Models/Character.cs
/// - CharacterReader (reading) - see Services/CharacterReader.cs
/// - CharacterWriter (writing) - see Services/CharacterWriter.cs
/// - Keep CharacterManager for just the menu/coordination
///
/// After refactoring, CharacterManager should:
/// - Create Reader and Writer objects
/// - Handle menu display and user input
/// - Call Reader/Writer methods to do the actual work
/// </summary>
public class CharacterManager
{
    private readonly string _filePath = "Files/input.csv";
    private string[] lines;

    public void AddCharacter()
    {
        // TODO: Replace this stub with code that prompts for character details,
        // adds the new character to the CSV file, and displays confirmation.
        // Use string interpolation for formatting output.
        // Hint: You will need to append the new character to the file.

        Console.Write("Enter character name: ");
        var name = Console.ReadLine();
        Console.Write("Enter character profession: ");
        var profession = Console.ReadLine();
        Console.Write("Enter character level: ");
        var level = Console.ReadLine();

        // TODO: Prompt for equipment items in a loop, not as a single pipe-separated string.
        var equipmentList = new List<string>();
        while (true)
        {
            Console.Write("Enter equipment item (leave blank to finish): ");
            var item = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(item))
            {
                break;
            }

            equipmentList.Add(item);
        }

        var equipment = string.Join("|", equipmentList);

        Console.WriteLine($"{name},{profession},{level},{equipment}");
        Console.WriteLine("Character added (stub).");
    }

    public void DisplayCharacters()
    {
        // TODO: Replace this stub with code that reads all character data from the CSV file
        // and displays each character.
        // Use string interpolation for formatting output.
        // Hint: You will need to parse each line and output the character details.
        Console.WriteLine("Displaying all characters...");
        Console.WriteLine("John, Brave,Fighter,1,10,sword|shield|potion");
        Console.WriteLine("Jane,Wizard,2,6,staff|robe|book");
        Console.WriteLine("Bob, Sneaky,Rogue,3,8,dagger|lockpick|cloak");
        Console.WriteLine("Alice,Cleric,4,12,mace|armor|potion");
        Console.WriteLine("Reginald III, Sir,Knight,5,20,sword|armor|horse");
    }

    public void FindCharacter()
    {
        // TODO: Replace this stub with code that prompts for a character name,
        // searches for the character using LINQ, and displays their details.
        // If not found, notify the user.
        // Use string interpolation for formatting output.
        // Hint: You will need to parse the CSV and use LINQ's FirstOrDefault.
        Console.Write("Enter character name to find: ");
        var name = Console.ReadLine();

        if (name == "Bob, Sneaky")
        {
            Console.WriteLine("Bob, Sneaky,Rogue,3,8,dagger|lockpick|cloak");
        }
        else
        {
            Console.WriteLine("Character not found");
        }
    }

    public void LevelUpCharacter()
    {
        // TODO: Replace this stub with code that prompts for a character name,
        // increases their level, updates the CSV file, and displays confirmation.
        // Use string interpolation for formatting output.
        // Hint: You will need to find the character, increment their level, and save changes.
        Console.Write("Enter character name to level up: ");
        var name = Console.ReadLine();

        if (name == "Reginald III, Sir")
        {
            Console.WriteLine("Reginald III, Sir is now level 6.");
        }
        else
        {
            Console.WriteLine($"{name} is now level X+1 (stub).");
        }
    }

    public void Run()
    {
        Console.WriteLine("Welcome to Character Management");

        lines = File.ReadAllLines(_filePath);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Find Character");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayCharacters();
                    break;
                case "2":
                    AddCharacter();
                    break;
                case "3":
                    LevelUpCharacter();
                    break;
                case "4":
                    FindCharacter();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
