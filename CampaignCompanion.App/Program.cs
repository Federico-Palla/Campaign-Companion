using CampaignCompanion.App;
using CampaignCompanion.Core;
using CampaignCompanion.Core.Catalog;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        string exportedCompendiaFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exports");
        if (!Directory.Exists(exportedCompendiaFolder))
        {
            Directory.CreateDirectory(exportedCompendiaFolder);
        }

        Console.WriteLine("========== Welcome to Character Companion Console App ==========");
        Console.WriteLine("V. 0.0.1");

        CompendiumManager manager = new CompendiumManager();
        ConsoleCommandInterpreter interpreter = new ConsoleCommandInterpreter(manager);

        // Load available compendia
        foreach (var compendiumFilePath in Directory.GetFiles(exportedCompendiaFolder, "*.json"))
        {
            string json = File.ReadAllText(compendiumFilePath);
            CompendiumDefinition? definition = JsonSerializer.Deserialize<CompendiumDefinition>(json);
            if (definition != null)
            {
                manager.Compendia.Add(new Compendium(definition));
            }
        }

        Console.WriteLine("Type 'exit', 'quit' or 'q' to close the application.\n");
        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (input is null)
            {
                return;
            }

            string[] actualCommands = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (actualCommands.Length == 0)
            {
                continue;
            }

            if (actualCommands[0].Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                actualCommands[0].Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                actualCommands[0].Equals("q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Goodbye!");
                return;
            }

            interpreter.ParseCommand(actualCommands);
        }
    }
}
