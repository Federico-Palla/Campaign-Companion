namespace CampaignCompanion.Core.Catalog;

/// <summary>
/// Base record of an entity present in a Compendium 
/// </summary>
public abstract record CatalogEntry
{
    public Guid Id { get; init; } = Guid.NewGuid();   // dal DB sovrascrivi, per l'homebrew nuovo è già pronto
    public required string Name { get; init; }
    public required int Version { get; init; }
    public string Description { get; init; } = string.Empty;
    public Guid CompendiumId { get; set; }
}
