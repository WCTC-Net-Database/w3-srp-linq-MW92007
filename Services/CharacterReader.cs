using W03.Models;

namespace W03.Services;

/// <summary>
/// CharacterReader - Responsible for ONE thing: READING character data.
///
/// This class demonstrates the Single Responsibility Principle (SRP):
/// - It ONLY reads character data from files
/// - It does NOT write data (that's CharacterWriter's job)
/// - It does NOT handle the menu or user interaction
///
/// By separating reading from writing, we can:
/// 1. Test reading independently
/// 2. Change how we read without affecting how we write
/// 3. Reuse this class anywhere we need to read characters
/// </summary>
public class CharacterReader
{
    private readonly string _filePath;

    /// <summary>
    /// Creates a new CharacterReader for the specified file.
    /// </summary>
    /// <param name="filePath">Path to the CSV file containing character data</param>
    public CharacterReader(string filePath)
    {
        _filePath = filePath;
    }

    /// <summary>
    /// Reads all characters from the CSV file.
    ///
    /// TODO: Implement this method to:
    /// 1. Read all lines from the file
    /// 2. Parse each line into a Character object
    /// 3. Return the list of characters
    ///
    /// HINT: Remember that names may contain commas (e.g., "John, Brave")
    /// You'll need to handle quoted fields from Week 2!
    /// </summary>
    /// <returns>A list of all characters in the file</returns>
    public List<Character> ReadAll()
    {
        var characters = new List<Character>();

        string[] lines = File.ReadAllLines(_filePath);

        foreach (string line in lines)
        {
            if (line == "Name,Profession,Level,HP,Equipment")
            {
                continue;
            }

            Character character = ParseLine(line);
            characters.Add(character);
        }

        // TODO: Read all lines from file
        // string[] lines = File.ReadAllLines(_filePath);

        // TODO: Skip header if present, then parse each line
        // foreach (string line in lines)
        // {
        //     Character character = ParseLine(line);
        //     characters.Add(character);
        // }

        return characters;
    }

    /// <summary>
    /// Finds a character by name using LINQ.
    ///
    /// This is where you'll use LINQ's FirstOrDefault method!
    ///
    /// LINQ Example:
    /// var result = characters.FirstOrDefault(c => c.Name == name);
    ///
    /// FirstOrDefault returns:
    /// - The first character that matches the condition, OR
    /// - null if no character matches
    /// </summary>
    /// <param name="characters">The list of characters to search</param>
    /// <param name="name">The name to search for</param>
    /// <returns>The matching character, or null if not found</returns>
    public Character? FindByName(string name)
    {
        var characters = File.ReadAllLines(_filePath).Skip(1).Select(ParseLine).ToList();
         
        return characters.FirstOrDefault(c => c.Name != null && c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Finds all characters matching a condition using LINQ.
    ///
    /// BONUS: This shows LINQ's Where method for filtering.
    ///
    /// LINQ Example:
    /// var warriors = characters.Where(c => c.Profession == "Fighter").ToList();
    /// </summary>
    /// <param name="characters">The list of characters to search</param>
    /// <param name="profession">The profession to filter by</param>
    /// <returns>All characters of the specified profession</returns>
    public List<Character> FindByProfession(List<Character> characters, string profession)
    {
        // TODO: Use LINQ Where to filter characters
        // return characters.Where(c => c.Profession == profession).ToList();

        return characters.Where(c => c.Profession == profession).ToList();
    }

    /// <summary>
    /// Helper method to parse a single CSV line into a Character object.
    ///
    /// HINT: This is similar to your Week 2 parsing, but now you're
    /// creating a Character object instead of just printing values.
    /// </summary>
    private Character ParseLine(string line)
    {
        // Parse CSV line, handling quoted fields and splitting equipment
        // Handles quoted fields and commas inside quotes
        List<string> fields = new List<string>();
        bool inQuotes = false;
        int start = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (line[i] == ',' && !inQuotes)
            {
                fields.Add(line.Substring(start, i - start));
                start = i + 1;
            }
        }
        fields.Add(line.Substring(start));

        string name = fields[0].Trim().Trim('\"', ',');
        name = name.Replace(",", string.Empty);

        string profession = fields[1].Trim();
        int level = int.Parse(fields[2].Trim());
        int hp = int.Parse(fields[3].Trim());
        string[] equipment = fields[4].Split('|');

        return new Character
        {
            Name = name,
            Profession = profession,
            Level = level,
            HP = hp,
            Equipment = equipment
        };
    }
}
