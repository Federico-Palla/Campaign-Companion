namespace CampaignCompanion.Core.Catalog;

/// <summary>
/// Base record of an entity present in a Compendium 
/// </summary>
public abstract class CatalogEntry
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
    public string Tag { get; set; } = string.Empty;
    public required string Version { get; init; }
    public string Description { get; init; } = string.Empty;
    public Guid CompendiumId { get; set; }
    public required Dictionary<string, string> Dependencies { get; init; } = [];

    public virtual bool Validate(out string errorMessage)
    {
        errorMessage = string.Empty;
        return true;
    }
}

public readonly record struct CatalogDependency
{
    public required string Tag { get; init; }        // Es. "core:spellschool:evocation"
    public required string VersionRange { get; init; } // Es. "^1.0.0"
}
