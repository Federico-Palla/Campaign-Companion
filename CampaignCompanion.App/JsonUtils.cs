using CampaignCompanion.Core;
using CampaignCompanion.Core.Catalog;
using System.Reflection;
using System.Text.Json;

namespace CampaignCompanion.App
{
    public sealed record CompendiumFileRoot
    {
        public required CompendiumDefinition Compendium { get; init; }
    }

    public static class CompendiumJSONExtensions
    {
        private static readonly IReadOnlyDictionary<string, Type> CatalogEntryTypesBySection =
            typeof(CatalogEntry).Assembly.GetTypes()
                .Where(type => !type.IsAbstract && typeof(CatalogEntry).IsAssignableFrom(type))
                .Select(type =>
                {
                    FieldInfo sectionField = type.GetField(
                        nameof(CatalogEntry.SectionName),
                        BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                        ?? throw new InvalidOperationException($"Catalog entry '{type.FullName}' does not declare a SectionName constant.");

                    return (SectionName: (string)sectionField.GetRawConstantValue()!, EntryType: type);
                })
                .ToDictionary(entry => entry.SectionName, entry => entry.EntryType, StringComparer.OrdinalIgnoreCase);

        public static void AddFromJSONListItems(this Compendium compendium, JsonElement categoryElement, JsonSerializerOptions options)
        {
            foreach (var section in categoryElement.EnumerateObject())
            {
                if (!CatalogEntryTypesBySection.TryGetValue(section.Name, out var entryType))
                    continue;

                foreach (var entryElement in section.Value.EnumerateArray())
                {
                    var entry = JsonSerializer.Deserialize(entryElement.GetRawText(), entryType, options) as CatalogEntry
                        ?? throw new JsonException($"Failed to deserialize '{section.Name}' catalog entry as '{entryType.Name}'.");
                    compendium.Add(entry);
                }
            }
        }
    }
}
