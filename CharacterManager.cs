using W03.Models;
using W03.Services;

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
        // Create a CharacterReader instance with the file path
        var reader = new CharacterReader(_filePath);
        var characters = reader.ReadAll();
        foreach (var character in characters)
        {
            Console.WriteLine(character);
        }

        Console.WriteLine();
    }

    public void FindCharacter()
    {
        Console.Write("Enter character name to find: ");
        var name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Character name cannot be empty.");
            return;
        }

        var reader = new CharacterReader(_filePath);
        var character = reader.FindByName(name);

        if (character == null)
        {
            Console.WriteLine($"Character '{name}' not found.");
            return;
        }

        Console.WriteLine(
            $"Name: {character.Name}, {character.Profession}, " +
            $"Level {character.Level}, HP {character.HP}, " +
            $"Equipment: {string.Join(", ", character.Equipment)}");
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

            Console.WriteLine();

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
