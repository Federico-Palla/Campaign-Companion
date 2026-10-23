namespace CampaignCompanion.App.Core.Catalog;

public abstract record CatalogEntry
{
    public Guid Id { get; init; } = Guid.NewGuid();   // dal DB sovrascrivi, per l'homebrew nuovo è già pronto
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public required Guid CompendiumId { get; init; }
}
