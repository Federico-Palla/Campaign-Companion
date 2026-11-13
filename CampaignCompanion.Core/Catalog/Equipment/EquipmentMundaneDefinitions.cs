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

    public override bool Validate(out string errorMessage)
    {
        errorMessage = string.Empty;
        if (!Dependencies.ContainsKey(Category))
        {
            errorMessage = "Invalid tool category specified.";
            return false;
        }

        if (!Dependencies.ContainsKey(Ability))
        {
            errorMessage = "Invalid ability specified.";
            return false;
        }

        if (!CraftableItems.All(item => Dependencies.ContainsKey(item)))
        {
            errorMessage = "Invalid craftable item specified.";
            return false;
        }

        return true;
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

    public override bool Validate(out string errorMessage)
    {
        errorMessage = string.Empty;
        if (!Dependencies.ContainsKey(Category))
        {
            errorMessage = "Invalid armor category specified.";
            return false;
        }

        return true;
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

    public override bool Validate(out string errorMessage)
    {
        errorMessage = string.Empty;
        if (!Dependencies.ContainsKey(Category))
        {
            errorMessage = "Invalid weapon category specified.";
            return false;
        }

        if (!WeaponProperties.All(item => Dependencies.ContainsKey(item.Property)))
        {
            errorMessage = "Invalid weapon property specified.";
            return false;
        }

        return true;
    }
}

public sealed class PoisonDefinition : MundaneItemDefinition;
public sealed class CreatureDefinition : MundaneItemDefinition
{
    public bool IsMount { get; set; } = false;
    public int CarryCapacity { get; set; } = 0; 
    public string? Barding { get; set; }

    public override bool Validate(out string errorMessage)
    {
        errorMessage = string.Empty;
        if (!string.IsNullOrEmpty(Barding) && !Dependencies.ContainsKey((string)Barding))
        {
            errorMessage = "Invalid barding specified.";
            return false;
        }

        return true;
    }
}


