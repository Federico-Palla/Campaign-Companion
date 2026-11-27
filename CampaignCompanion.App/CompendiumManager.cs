using CampaignCompanion.Core;
using CampaignCompanion.Core.Catalog;
using System.Text.Json;

namespace CampaignCompanion.App;

public class CompendiumManager
{
    public List<Compendium> Compendia = [];

    // Fast O(1) lookup HashSet for enabled entries
    public HashSet<CatalogEntry> Entries = [];

    // Track enabled compendium Tags to prevent redundant work and circular dependency loops
    public HashSet<string> EnabledCompendia = [];

    public string CompendiaFolder { get; }

    public CompendiumManager()
    {
        CompendiaFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exports");
        if (!Directory.Exists(CompendiaFolder))
        {
            Directory.CreateDirectory(CompendiaFolder);
        }
    }

    public void LoadCompendiaFromLocalJson()
    {
        string basePath = AppContext.BaseDirectory;
        string jsonsDir = Path.Combine(basePath, "JSON");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (!Directory.Exists(jsonsDir)) return;

        string[] compendiaFiles = Directory.GetFiles(jsonsDir, "*.json");
        foreach (string path in compendiaFiles)
        {
            string jsonString = File.ReadAllText(path);

            using var doc = JsonDocument.Parse(jsonString);
            var rootElement = doc.RootElement;

            // 1. Parse Compendium Metadata
            if (!rootElement.TryGetProperty("Compendium", out var compendiumElement))
                continue;

            var compendiumDefinition = JsonSerializer.Deserialize<CompendiumDefinition>(compendiumElement.GetRawText(), options)
                                       ?? throw new Exception("Failed to deserialize Compendium definition.");

            var compendium = new Compendium(compendiumDefinition);

            // 2. Parse Dynamic Category Blocks (e.g., "Core", "Technomancy") and extract entries
            foreach (var property in rootElement.EnumerateObject())
            {
                if (property.Name.Equals("Compendium", StringComparison.OrdinalIgnoreCase))
                    continue;

                var categoryElement = property.Value;
                compendium.AddFromJSONListItems(categoryElement, options);
            }

            Compendia.Add(compendium);
        }
    }

    public void LoadCompendiumFromFile(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);

        using var doc = JsonDocument.Parse(jsonString);
        var rootElement = doc.RootElement;

        // 1. Parse Compendium Metadata
        if (!rootElement.TryGetProperty("Compendium", out var compendiumElement))
            return;

        var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
        var compendiumDefinition = JsonSerializer.Deserialize<CompendiumDefinition>(compendiumElement.GetRawText(), options)
                                   ?? throw new Exception("Failed to deserialize Compendium definition.");

        var compendium = new Compendium(compendiumDefinition);

        // 2. Parse Dynamic Category Blocks and extract entries
        foreach (var property in rootElement.EnumerateObject())
        {
            if (property.Name.Equals("Compendium", StringComparison.OrdinalIgnoreCase))
                continue;

            var categoryElement = property.Value;

            compendium.AddFromJSONListItems(categoryElement, options);
        }

        Compendia.Add(compendium);
    }

    public void EnableCompendiumByTag(string tag)
    {
        // Avoid reprocessing if already enabled
        if (EnabledCompendia.Contains(tag)) return;

        try
        {
            Compendium? selected = Compendia.FirstOrDefault(c => c.Tag == tag)
                                   ?? throw new Exception($"Unable to find Compendium with ID: {tag}");

            // Recursively enable all dependencies first
            foreach (var dependency in selected.Dependencies)
            {
                EnableCompendiumByTag(dependency.Tag);
            }

            // UnionWith adds all entries efficiently into the HashSet without duplicates
            Entries.UnionWith(selected.All());
            EnabledCompendia.Add(tag);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    //
    public Compendium? GetById(string id) => Compendia.FirstOrDefault(x => x.Id == new Guid(id));
    public Compendium? GetByName(string name) => Compendia.FirstOrDefault(x => x.Name == name);
    public Compendium? GetByTag(string tag) => Compendia.FirstOrDefault(x => x.Tag == tag);
}