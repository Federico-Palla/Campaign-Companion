namespace CampaignCompanion.Core.Catalog.Feats;

public sealed class FeatCategoryDefinition : CatalogEntry;

public class FeatDefinition : CatalogEntry
{
    public required string Category { get; set; }
    public IReadOnlyList<string> Grants { get; set; } = [];
    public IReadOnlyList<string> Rules { get; set; } = [];
}