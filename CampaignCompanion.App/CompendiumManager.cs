using CampaignCompanion.Core;
using CampaignCompanion.Core.Catalog;
using System.Text.Json;

namespace CampaignCompanion.App;

public class CompendiumManager
{
    public List<Compendium> Compendia = [];

    // Fast O(1) lookup HashSet for enabled entries
    public HashSet<CatalogEntry> Entries = [];

    // Track enabled compendium IDs to prevent redundant work and circular dependency loops
    public HashSet<Guid> EnabledCompendiaIds = [];

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

                // Parse Abilities if present
                if (categoryElement.TryGetProperty("Abilities", out var abilitiesElement))
                {
                    var abilities = JsonSerializer.Deserialize<List<AbilityDefinition>>(abilitiesElement.GetRawText(), options);
                    if (abilities != null)
                    {
                        foreach (var ability in abilities) compendium.Add(ability);
                    }
                }

                // Parse Skills if present
                if (categoryElement.TryGetProperty("Skills", out var skillsElement))
                {
                    var skills = JsonSerializer.Deserialize<List<SkillDefinition>>(skillsElement.GetRawText(), options);
                    if (skills != null)
                    {
                        foreach (var skill in skills) compendium.Add(skill);
                    }
                }

                // Parse DamageTypes if present
                if (categoryElement.TryGetProperty("DamageTypes", out var damageTypesElement))
                {
                    var damageTypes = JsonSerializer.Deserialize<List<DamageTypeDefinition>>(damageTypesElement.GetRawText(), options);
                    if (damageTypes != null)
                    {
                        foreach (var damageType in damageTypes) compendium.Add(damageType);
                    }
                }
            }

            Compendia.Add(compendium);
        }
    }

    public void TryEnableCompendiumById(Guid id)
    {
        // Avoid reprocessing if already enabled
        if (EnabledCompendiaIds.Contains(id)) return;

        try
        {
            Compendium? selected = Compendia.FirstOrDefault(c => c.Id == id)
                                   ?? throw new Exception($"Unable to find Compendium with ID: {id}");

            // Recursively enable all dependencies first
            foreach (var dependency in selected.Dependencies)
            {
                TryEnableCompendiumById(dependency);
            }

            // UnionWith adds all entries efficiently into the HashSet without duplicates
            Entries.UnionWith(selected.All());
            EnabledCompendiaIds.Add(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}