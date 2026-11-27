using CampaignCompanion.Core;
using CampaignCompanion.Core.Catalog;
using System.Text.Json;

namespace CampaignCompanion.App
{
    public class ConsoleCommandInterpreter(CompendiumManager manager)
    {
        public CompendiumManager Manager { get; } = manager;

        public void ParseCommand(string[] commands)
        {
            string[] arguments = commands
                .Where(command => !string.IsNullOrWhiteSpace(command))
                .ToArray();

            if (arguments.Length == 0)
            {
                return;
            }

            switch (arguments[0].ToLowerInvariant())
            {
                case "list":
                    if (arguments.Length != 1)
                    {
                        Console.WriteLine("Usage: list");
                        return;
                    }

                    Console.WriteLine("You currently have {0} defined Compendia:", Manager.Compendia.Count);
                    foreach (var compendium in Manager.Compendia)
                    {
                        Console.WriteLine("+ [{0}] {1} ({2})", compendium.Tag, compendium.Name, compendium.Version);
                    }
                    break;

                case "show":
                    ShowCompendium(arguments);
                    break;

                case "create-new":
                    if (arguments.Length != 1)
                    {
                        Console.WriteLine("Usage: create-new");
                        return;
                    }

                    CreateCompendium(Manager);
                    break;

                case "export":
                    ExportCompendia(arguments);
                    break;

                case "help":
                    PrintHelp();
                    break;

                default:
                    Console.WriteLine("Unknown command '{0}'. Type 'help' to see available commands.", arguments[0]);
                    break;
            }
        }

        private void ExportCompendia(string[] commands)
        {
            if (commands.Length < 2)
            {
                Console.WriteLine("Usage: export --all | export --tag <tag>");
                return;
            }

            if (commands.Length == 2 && commands[1] == "--all")
            {
                Console.WriteLine("Do you whish to export all Compendia? (y/n)");
                string answer = Console.ReadLine() ?? "n";
                if (answer.Equals("y", StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (var compendium in Manager.Compendia)
                    {
                        // Export Compendium
                        var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                        string json = JsonSerializer.Serialize(compendium, options);

                        string filePath = Path.Combine(Manager.CompendiaFolder, $"{compendium.Tag}.json");
                        File.WriteAllText(filePath, json);

                        Console.WriteLine("Compendium {0} has been successfuly exported in {1}", compendium.Tag, filePath);

                    }
                }                
            }
            else
            {
                switch (commands[1].ToLowerInvariant())
                {
                    case "--tag" when commands.Length == 3:
                        var compendium = Manager.GetByTag(commands[2]);
                        if(compendium == null)
                        {
                            Console.WriteLine("Usage: export --all | export --tag <tag>");
                            return;
                        }
                        var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
                        string json = JsonSerializer.Serialize(compendium, options);

                        string filePath = Path.Combine(Manager.CompendiaFolder, $"{compendium.Tag}.json");
                        File.WriteAllText(filePath, json);

                        Console.WriteLine("Compendium {0} has been successfuly exported in {1}", compendium.Tag, filePath);
                        break;
                    default:
                        Console.WriteLine("Usage: export --all | export --tag <tag>");
                        return;
                }
            }
        }


        private void ShowCompendium(string[] commands)
        {
            if (commands.Length < 2)
            {
                Console.WriteLine("Usage: show <tag> | show --tag <tag> | show --id <id> | show --name <name>");
                return;
            }

            Compendium? compendium;
            if (commands.Length == 2)
            {
                compendium = Manager.GetByTag(commands[1]);
            }
            else
            {
                switch (commands[1].ToLowerInvariant())
                {
                    case "--tag" when commands.Length == 3:
                        compendium = Manager.GetByTag(commands[2]);
                        break;
                    case "--id" when commands.Length == 3:
                        if (!Guid.TryParse(commands[2], out Guid id))
                        {
                            Console.WriteLine("'{0}' is not a valid Compendium ID.", commands[2]);
                            return;
                        }

                        compendium = Manager.Compendia.FirstOrDefault(item => item.Id == id);
                        break;
                    case "--name" when commands.Length >= 3:
                        compendium = Manager.GetByName(string.Join(' ', commands.Skip(2)));
                        break;
                    default:
                        Console.WriteLine("Usage: show <tag> | show --tag <tag> | show --id <id> | show --name <name>");
                        return;
                }
            }

            if (compendium is null)
            {
                Console.WriteLine("No matching Compendium was found.");
                return;
            }

            Console.WriteLine(compendium);
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  list                         List all Compendia");
            Console.WriteLine("  show <tag>                   Show a Compendium by tag");
            Console.WriteLine("  show --tag <tag>             Show a Compendium by tag");
            Console.WriteLine("  show --id <id>               Show a Compendium by ID");
            Console.WriteLine("  show --name <name>           Show a Compendium by name");
            Console.WriteLine("  create-new                   Create a Compendium");
            Console.WriteLine("  help                         Show this help");
            Console.WriteLine("  exit | quit                  Close the application");
        }

        private static CompendiumDefinition CreateCompendium(CompendiumManager manager)
        {
            Console.WriteLine("===== Compendium Creation =====");
            string compendiumName = EnterStringValue("Name");
            string compendiumTag = compendiumName.ToLower().Replace(" ", "-");
            int count = 1;
            if (manager.Compendia.Any(c => c.Tag == compendiumTag))
            {
                compendiumTag += "_" + count++;
            }
            Console.Write("Given this Compendium Name the Tag suggested is {0}, do you confirm? (y/n): ", compendiumTag);
            string TagAnswer = Console.ReadLine() ?? "n";
            if (TagAnswer.ToLower() != "y")
            {
                compendiumTag = EnterStringValue("Tag");
            }

            if (manager.Compendia.Any(c => c.Tag == compendiumTag))
            {
                Console.Write("[Error] Tag {0} is already present, you need to define a new one: ");
                compendiumTag = EnterStringValue("Tag");
            }

            string compendiumDescription = EnterStringValue("Description", true);
            string defaultCreationVersion = "v.1.0.0";
            Dictionary<string, string> dependencies = CollectDependencies(manager, compendiumTag);

            var newCompendium = new CompendiumDefinition()
            {
                Name = compendiumName,
                Tag = compendiumTag,
                Description = compendiumDescription,
                Version = defaultCreationVersion,
                Dependencies = dependencies,
                CreationDate = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };

            // stringification
            Console.WriteLine("===== Compendium has been Created =====");
            Compendium compendium = new Compendium(newCompendium);
            Console.WriteLine(compendium.ToString());
            manager.Compendia.Add(compendium);

            return newCompendium;
        }

        private static Dictionary<string, string> CollectDependencies(CompendiumManager manager, string compendiumTag)
        {
            var dependencies = new Dictionary<string, string>();
            Compendium[] availableCompendia = manager.Compendia
                .Where(compendium => !compendium.Tag.Equals(compendiumTag, StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (availableCompendia.Length == 0)
            {
                Console.WriteLine("No existing Compendia are available as dependencies.");
                return dependencies;
            }

            Console.Write("Do you want to add any dependencies for this Compendium? (y/n): ");
            string? answer = Console.ReadLine();
            while (answer is not null &&
                   !answer.Equals("y", StringComparison.OrdinalIgnoreCase) &&
                   !answer.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Please enter 'y' or 'n': ");
                answer = Console.ReadLine();
            }

            if (answer is null || answer.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                return dependencies;
            }

            Console.WriteLine("Available Compendia:");
            foreach (var compendium in availableCompendia)
            {
                Console.WriteLine("  [{0}] {1} ({2})", compendium.Tag, compendium.Name, compendium.Version);
            }

            while (true)
            {
                Console.Write("Enter a dependency tag (or press Enter to finish): ");
                string? tag = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(tag))
                {
                    break;
                }

                Compendium? dependency = availableCompendia.FirstOrDefault(compendium =>
                    compendium.Tag.Equals(tag, StringComparison.OrdinalIgnoreCase));
                if (dependency is null)
                {
                    Console.WriteLine("No existing Compendium has tag '{0}'.", tag);
                    continue;
                }

                if (dependencies.ContainsKey(dependency.Tag))
                {
                    Console.WriteLine("Dependency '{0}' has already been added.", dependency.Tag);
                    continue;
                }

                string versionRange = EnterStringValue($"Version range for {dependency.Tag}");
                dependencies.Add(dependency.Tag, versionRange);
            }

            return dependencies;
        }

        private static string EnterStringValue(string label, bool optional = false)
        {
            Console.WriteLine("Enter {0}{1}: ", label, optional ? "(optional)" : "");
            string value = Console.ReadLine() ?? "";
            if (!optional)
            {
                while (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    Console.Write("\nYou need to enter a valid {0}: ", label);
                    value = Console.ReadLine() ?? "";
                }
            }

            return value;
        }
    }
}
