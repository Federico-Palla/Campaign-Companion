using CampaignCompanion.Core;
using CampaignCompanion.Core.Catalog;
using System.Text.Json;

namespace CampaignCompanion.App
{
    public sealed record CompendiumFileRoot
    {
        public required CompendiumDefinition Compendium { get; init; }
    }

    public static class CompendiumJSONExtensions
    {
        public static void AddFromJSONListItem<T>(this Compendium compendium, string property, JsonElement categoryElement, JsonSerializerOptions options) where T : CatalogEntry
        {
            if (categoryElement.TryGetProperty(property, out var element))
            {
                var catalogentries = JsonSerializer.Deserialize<List<T>>(element.GetRawText(), options);
                if (catalogentries != null)
                {
                    foreach (var entry in catalogentries) compendium.Add(entry);
                }
            }
        }
    }
}
