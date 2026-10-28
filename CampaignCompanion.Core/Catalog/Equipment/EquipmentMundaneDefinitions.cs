namespace CampaignCompanion.Core.Catalog.Equipment;

// TODO: adventurer's gear

public record MundaneItemDefinition : ItemDefinition;

/// <summary>
/// I.e. Artisan Tools ot Musical Instrument
/// </summary>
public sealed record ToolCategoryDefinition : CatalogEntry;
public sealed record ToolDefinition : MundaneItemDefinition
{
    public required ToolCategoryDefinition Category { get; set; }
    public required AbilityDefinition Ability { get; set; }
    public required string Utilize { get; set; }
    public IReadOnlyList<MundaneItemDefinition> CraftableItems { get; set; } = [];
}

public sealed record ArmorCategoryDefinition : CatalogEntry;
public sealed record ArmorDefinition : MundaneItemDefinition
{
    public required ArmorCategoryDefinition Category { get; set; }
    public required int BaseArmorClass { get; set; }
    public required int AdditionalModifier { get; set; }
    public bool UseDexterity { get; set; } = false;
    public int DexterityModifierCap { get; set; } = 0;
    public int StregthRequirement { get; set; } = 0;
    public bool StealthDisadvantage { get; set; } = false;
}

public sealed record WeaponCategoryDefinition : CatalogEntry;
public sealed record WeaponPropertyDefinition : CatalogEntry;
public sealed record WeaponMasteryDefinition : CatalogEntry;
public sealed record WeaponPropertyUsageDefinition
{
    public required WeaponPropertyDefinition Property { get; set; }
    public RangeInterval? Range { get; set; }
    public DiceRoll? AlternateRoll { get; set; }
}
public sealed record WeaponDefinition : MundaneItemDefinition
{
    public required WeaponCategoryDefinition Category { get; set; }
    public required IReadOnlyList<WeaponPropertyUsageDefinition> WeaponProperties { get; set; }
    public required DiceRoll Damage { get; set; }
    public bool IsRanged { get; set; } = false;
}

public sealed record PoisonDefinition : MundaneItemDefinition;
public sealed record CreatureDefinition : MundaneItemDefinition
{
    public bool IsMount { get; set; } = false;
    public int CarryCapacity { get; set; } = 0; 
    public ArmorDefinition? Barding { get; set; }
}


