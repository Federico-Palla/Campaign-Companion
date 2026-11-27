namespace CampaignCompanion.Core.Catalog;

/// <summary>
/// Base record of an entity present in a Compendium 
/// </summary>
public abstract class CatalogEntry
{
    public const string SectionName = "CatalogEntry";

    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string Version { get; set; } = "1.0.0";
    public string Description { get; set; } = string.Empty;
    public Dictionary<string, string> Dependencies { get; set; } = [];

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
