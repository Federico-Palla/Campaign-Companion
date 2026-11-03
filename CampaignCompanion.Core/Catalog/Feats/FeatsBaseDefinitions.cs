namespace CampaignCompanion.Core.Catalog.Feats;

public sealed class FeatCategoryDefinition : CatalogEntry;

public class FeatDefinition : CatalogEntry
{
    public required FeatCategoryDefinition Category { get; set; }
    //TODO
}