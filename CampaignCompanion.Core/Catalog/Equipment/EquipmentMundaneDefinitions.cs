namespace CampaignCompanion.Core.Catalog.Equipment;

// TODO: adventurer's gear

public class MundaneItemDefinition : ItemDefinition;

/// <summary>
/// I.e. Artisan Tools ot Musical Instrument
/// </summary>
public sealed class ToolCategoryDefinition : CatalogEntry;
public sealed class ToolDefinition : MundaneItemDefinition
{
    public required string Category { get; set; }
    public required string Ability { get; set; }
    public required string Utilize { get; set; }
    public IReadOnlyList<string> CraftableItems { get; set; } = [];

    public override bool Validate()
    {
        return Dependencies.ContainsKey(Category)
            && Dependencies.ContainsKey(Ability)
            && CraftableItems.All(item => Dependencies.ContainsKey(item));
    }
}

public sealed class ArmorCategoryDefinition : CatalogEntry;
public sealed class ArmorDefinition : MundaneItemDefinition
{
    public required string Category { get; set; }
    public required int BaseArmorClass { get; set; }
    public required int AdditionalModifier { get; set; }
    public bool UseDexterity { get; set; } = false;
    public int DexterityModifierCap { get; set; } = 0;
    public int StregthRequirement { get; set; } = 0;
    public bool StealthDisadvantage { get; set; } = false;

    public override bool Validate()
    {
        return Dependencies.ContainsKey(Category);
    }
}

public sealed class WeaponCategoryDefinition : CatalogEntry;
public sealed class WeaponPropertyDefinition : CatalogEntry;
public sealed class WeaponMasteryDefinition : CatalogEntry;
public sealed class WeaponPropertyUsageDefinition
{
    public required string Property { get; set; }
    public RangeInterval? Range { get; set; }
    public DiceRoll? AlternateRoll { get; set; }

}
public sealed class WeaponDefinition : MundaneItemDefinition
{
    public required string Category { get; set; }
    public required IReadOnlyList<WeaponPropertyUsageDefinition> WeaponProperties { get; set; }
    public required DiceRoll Damage { get; set; }
    public bool IsRanged { get; set; } = false;

    public override bool Validate()
    {
        return Dependencies.ContainsKey(Category)
            && WeaponProperties.All(item => Dependencies.ContainsKey(item.Property));
    }
}

public sealed class PoisonDefinition : MundaneItemDefinition;
public sealed class CreatureDefinition : MundaneItemDefinition
{
    public bool IsMount { get; set; } = false;
    public int CarryCapacity { get; set; } = 0; 
    public string? Barding { get; set; }

    public override bool Validate()
    {
        return string.IsNullOrEmpty(Barding) || Dependencies.ContainsKey((string)Barding);
    }
}


