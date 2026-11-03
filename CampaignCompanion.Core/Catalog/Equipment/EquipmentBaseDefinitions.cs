namespace CampaignCompanion.Core.Catalog.Equipment;

/// <summary>
/// Defines a type of item in the Compendium, like Weapon or Tool
/// </summary>
public sealed class ItemTypeDefinition : CatalogEntry;

/// <summary>
/// Defines an item Rarity, like Common or Legendary
/// </summary>
public sealed class ItemRarityDefinition : CatalogEntry;

/// <summary>
/// A basic definition of an Item managed via the Compendium
/// </summary>
public class ItemDefinition : CatalogEntry
{
    public required int Cost { get; init; }
    public required int Weight { get; init; }
    public required string Type { get; init; }

    public override bool Validate()
    {
        return Dependencies.ContainsKey(Type) && Cost >= 0 && Weight >= 0;
    }
}
