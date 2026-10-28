namespace CampaignCompanion.Core.Catalog.Feats;

public sealed record FeatCategoryDefinition : CatalogEntry;

public record FeatDefinition : CatalogEntry
{
    public required FeatCategoryDefinition Category { get; set; }
    //TODO
}