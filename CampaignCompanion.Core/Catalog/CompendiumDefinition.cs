namespace CampaignCompanion.Core.Catalog;

public sealed class CompendiumDefinition: CatalogEntry
{
    public new const string SectionName = "Compendium";

    public DateTime CreationDate { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}