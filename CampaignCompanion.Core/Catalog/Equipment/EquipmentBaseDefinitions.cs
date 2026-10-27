namespace CampaignCompanion.Core.Catalog.Equipment;

/// <summary>
/// Defines a type of item in the Compendium, like Weapon or Tool
/// </summary>
public sealed record ItemTypeDefinition : CatalogEntry;

/// <summary>
/// Defines an item Rarity, like Common or Legendary
/// </summary>
public sealed record ItemRarityDefinition : CatalogEntry;

/// <summary>
/// A basic definition of an Item managed via the Compendium
/// </summary>
public record ItemDefinition : CatalogEntry
{
    public required int Cost { get; init; }
    public required int Weight { get; init; }
    public required ItemTypeDefinition Type { get; init; }
}
